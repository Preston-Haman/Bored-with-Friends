using BoredWithFriends.Network.Packets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoredWithFriends.Network
{
	/// <summary>
	/// An Implementation of <see cref="NetworkHandler"/> that acts as the network
	/// management for this application when it is running as a server. A
	/// <see cref="TcpListener"/> is used to listen for incoming connections;
	/// this is done on a separate thread in a blocking fashion.
	/// <br></br><br></br>
	/// When this implementation is told to run in local mode, it will bind to
	/// the machine's loopback address. When not in local mode, it will listen
	/// on all available interfaces. In either case, port 7777 is used.
	/// </summary>
	internal class ServerNetworkHandler : NetworkHandler
	{
		/// <summary>
		/// The <see cref="TcpListener"/> that will listen for incoming conections.
		/// </summary>
		private readonly TcpListener server;

		/// <summary>
		/// The Thread that <see cref="server"/> will listen on.
		/// </summary>
		private Thread? listener;

		/// <summary>
		/// Creates a new <see cref="ServerNetworkHandler"/>. It will not be started
		/// until a call to <see cref="Start"/> is made.
		/// </summary>
		/// <param name="localMode">If true, this will run in a local mode on the loopback
		/// address of the running machine.</param>
		public ServerNetworkHandler(bool localMode = false) : base("Server")
		{
			server = localMode ? new TcpListener(IPAddress.Loopback, 7777) : new TcpListener(IPAddress.Any, 7777);
		}

		~ServerNetworkHandler()
		{
			Stop();
		}

		/// <inheritdoc/>
		public override void Start()
		{
			if (!IsStarted)
			{
				base.Start();
				listener = new(new ThreadStart(ListenForConnections));
				listener.Name = "Server Connection Listener";
				listener.IsBackground = true;
				listener.Start();
			}
		}

		public override void Stop()
		{
			if (IsStarted)
			{
				listener!.Interrupt();
				base.Stop();
			}
		}

		/// <summary>
		/// Starts <see cref="server"/> and begins listening for incoming connections
		/// on an infinite loop. When new connections are accepted, they are then registered
		/// via <see cref="NetworkHandler.AddConnection"/>.
		/// </summary>
		protected void ListenForConnections()
		{
			try
			{
				server.Start();
				while (true)
				{
					ClientConnection con = new(server.AcceptTcpClient());
					AddConnection(con);
					//TODO: Send packet out on con asking for handshake.
				}
			}
			catch (ThreadInterruptedException)
			{
				//Shutdown.
				server.Stop();
			}
		}
	}

	/// <summary>
	/// An Implementation of <see cref="NetworkHandler"/> that acts as the network
	/// management for this application when it is running as a client.
	/// </summary>
	internal class ClientNetworkHandler : NetworkHandler
	{
		private ServerConnection? serverConnection = null;

		private Thread? connectionThread = null;

		public ClientNetworkHandler() : base("Client")
		{
			//Nothing to do
		}

		~ClientNetworkHandler()
		{
			Stop();
		}

		public override void Stop()
		{
			CloseServerConnection();
			base.Stop();
		}

		private void CloseServerConnection()
		{
			lock (this)
			{
				if (connectionThread is not null)
				{
					connectionThread.Interrupt();
				}

				if (serverConnection is not null)
				{
					serverConnection.Close();
					serverConnection = null;
				}
			}
		}

		public void ConnectToServer(string playerUsername, string password, bool createNew = false, string serverIP = "127.0.0.1", int port = 7777)
		{
			CloseServerConnection();

			connectionThread = new(new ThreadStart(CreateConnection));
			connectionThread.Name = "Server Handshake";
			connectionThread.IsBackground = true;
			connectionThread.Start();

			void CreateConnection()
			{
				try
				{
					while (serverConnection is null)
					{
						try
						{
							//This is a blocking call
							serverConnection = new(serverIP, port);
						}
						catch (ThreadInterruptedException)
						{
							//We failed to connect before the client gave up.
							serverConnection = null; //I don't think this will do anything.

							//This thread is about to terminate, we don't need a reference to it anymore.
							lock (connectionThread!)
							{
								if (connectionThread == Thread.CurrentThread)
								{
									connectionThread = null;
								}
							}
							return;
						}
						catch
						{
							//Try again in a moment.
							Thread.Sleep(100);
						}
					}
					AddConnection(serverConnection);
				}
				catch (ThreadInterruptedException)
				{
					//Catching twice for safety
				}

				//This thread is about to terminate, we don't need a reference to it anymore.
				lock (connectionThread!)
				{
					if (connectionThread == Thread.CurrentThread)
					{
						connectionThread = null;
					}
				}
			}
		}
	}

	/// <summary>
	/// A base class for handling <see cref="Connection"/> and <see cref="BasePacket"/> classes.
	/// <br></br><br></br>
	/// This abstract implementation mostly focuses on looping through all registered Connections;
	/// reading any incoming data, and writing any queued outgoing data -- in that order -- on a
	/// single thread that is created by this implementation.
	/// </summary>
	internal abstract class NetworkHandler
	{
		private readonly string handlerName;

		/// <summary>
		/// A Queue of Connections that have been registered to this handler.
		/// </summary>
		private readonly ConcurrentQueue<Connection> connections = new();

		/// <summary>
		/// A Queue of outbound packets, along with the Connection they should
		/// be transmitted out to.
		/// </summary>
		private readonly ConcurrentQueue<Tuple<Connection, BasePacket>> packetsToSend = new();

		/// <summary>
		/// A reference to the Thread that reads and writes network data.
		/// <br></br><br></br>
		/// This Thread is declared as a Background Thread; it will not keep
		/// the application open if all foreground threads have halted.
		/// </summary>
		private Thread? dispatchThread;

		private Tuple<bool, bool> isStartedAndIsStopping = new(false, false);

		/// <summary>
		/// Tracks if this handler has been started or not.
		/// </summary>
		protected bool IsStarted
		{
			get
			{
				return isStartedAndIsStopping.Item1;
			}

			set
			{
				lock (isStartedAndIsStopping)
				{
					isStartedAndIsStopping = new(value, isStartedAndIsStopping.Item2);
				}
			}
		}

		private bool IsStopping
		{
			get
			{
				return isStartedAndIsStopping.Item2;
			}

			set
			{
				lock (isStartedAndIsStopping)
				{
					isStartedAndIsStopping = new(isStartedAndIsStopping.Item1, value);
				}
			}
		}

		/// <summary>
		/// Base constructor which takes a <paramref name="handlerName"/> for the thread this implementation
		/// spawns.
		/// <br></br><br></br>
		/// It is used to name the thread as if by
		/// <code>Thread.Name = $"{<paramref name="handlerName"/>} Read/Write Dispatcher"</code>
		/// </summary>
		/// <param name="handlerName"></param>
		public NetworkHandler(string handlerName)
		{
			this.handlerName = handlerName;
		}

		~NetworkHandler()
		{
			Stop();
		}

		/// <summary>
		/// Registers a connection to read from with this handler.
		/// <br></br><br></br>
		/// If the <see cref="dispatchThread"/> has not been started, it will be.
		/// </summary>
		/// <param name="con">The connection to register.</param>
		public void AddConnection(Connection con)
		{
			connections.Enqueue(con);
			StartDispatchThread();
		}

		/// <summary>
		/// Registers a <paramref name="packet"/> to write out to <paramref name="con"/>.
		/// <br></br><br></br>
		/// The packet is queued until the next Read/Write loop occurs (see <see cref="ReadWriteDispatch"/>).
		/// </summary>
		/// <param name="con">The connection to write the <paramref name="packet"/> out to.</param>
		/// <param name="packet">The packet to write out on <paramref name="con"/>.</param>
		public void SendPacket(Connection con, BasePacket packet)
		{
			packetsToSend.Enqueue(new Tuple<Connection, BasePacket>(con, packet));
		}

		/// <summary>
		/// Starts this handler's separate threads; this begins the handling of
		/// networking aspects that have been registered.
		/// <br></br><br></br>
		/// Subclasses should check <see cref="IsStarted"/> before performing any
		/// operations that interact with other threads. The base implementation
		/// locks on itself before setting <see cref="IsStarted"/> to true as a
		/// precaution.
		/// </summary>
		public virtual void Start()
		{
			lock (this)
			{
				while (IsStopping)
				{
					Thread.Sleep(5);
				}

				if (!IsStarted)
				{
					IsStarted = true;
					StartDispatchThread();
				}
			}
		}

		public virtual void Stop()
		{
			lock (this)
			{
				if (IsStarted)
				{
					IsStopping = true;
					dispatchThread!.Interrupt();
				}
			}
		}

		/// <summary>
		/// Starts the <see cref="dispatchThread"/>.
		/// </summary>
		protected void StartDispatchThread()
		{
			if (dispatchThread is null)
			{
				dispatchThread = new(new ThreadStart(ReadWriteDispatch));
				dispatchThread.Name = $"{handlerName} Read/Write Dispatcher";
				dispatchThread.IsBackground = true;
				dispatchThread.Start();
			}
		}

		/// <summary>
		/// An infinite loop that iterates through all registered connections, reads from
		/// them if possible, and then writes any queued outbound packets to the network.
		/// <br></br><br></br>
		/// The infinite loop attempts to pause for up to 80ms between iterations, and
		/// attempts to avoid blocking calls.
		/// </summary>
		private void ReadWriteDispatch()
		{
			try
			{
				Stopwatch stopwatch = new();
				while (true)
				{
					try
					{
						//If an exception happened
						if (stopwatch.ElapsedMilliseconds < 75)
						{
							/*
							 * Let the thread rest some. This isn't strictly necessary;
							 * I just don't want this thread to be going full bore if
							 * something goes wrong.
							 */
							Thread.Sleep((int) (80 - stopwatch.ElapsedMilliseconds));
						}

						stopwatch.Restart();

						//Read if possible
						for (int i = 0, count = connections.Count; i < count; i++)
						{
							if (connections.TryDequeue(out Connection? con))
							{
								if (con.IsOpen() && (BasePacket.Recieve(con) || con.IsOpen()))
								{
									//If the connection is open, and it wasn't rejected by BasePacket.Recieve
									//Then add it back to the queue; otherwise, abandon it.
									connections.Enqueue(con);
								}
							}
						}

						//Write if necessary
						for (int i = 0, count = packetsToSend.Count; i < count; i++)
						{
							if (packetsToSend.TryDequeue(out Tuple<Connection, BasePacket>? tuple))
							{
								if (!BasePacket.Send(tuple.Item1, tuple.Item2))
								{
									//Try again next time
									packetsToSend.Enqueue(tuple);
								}
							}
						}
						int sleepTime = (int) (80 - stopwatch.ElapsedMilliseconds);
						Thread.Sleep(sleepTime > 0 ? sleepTime : 20);
					}
					catch (ThreadInterruptedException)
					{
						break;
					}
					catch
					{
						//This is a safety net. This thread must not crash.
						Debug.Fail($"The Dispatch Thread <{Thread.CurrentThread.Name}> encountered an unknown problem!");
					}
				}
			}
			catch (ThreadInterruptedException)
			{
				//Catching twice for safety
			}
			lock (isStartedAndIsStopping)
			{
				try
				{
					while (connections.TryDequeue(out Connection? con))
					{
						if (con.IsOpen())
						{
							con.Close();
						}
					}

					while (packetsToSend.TryDequeue(out Tuple<Connection, BasePacket>? tuple))
					{
						if (tuple.Item1.IsOpen())
						{
							tuple.Item1.Close();
						}
					}
				}
				catch
				{
					//Leave it to the GC, I guess...
					connections.Clear();
					packetsToSend.Clear();
				}


				//Thread is about to exit, we don't need a reference to it anymore.
				lock (dispatchThread!)
				{
					if (dispatchThread == Thread.CurrentThread)
					{
						dispatchThread = null;
					}
				}

				IsStarted = false;
				IsStopping = false;
			}
		}
	}
}
