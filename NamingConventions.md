# Capitalization Rules for Identifiers
*Conventions taken from microsoft's guidelines, however we will be using camel for fields and variables as well*

To differentiate words in an identifier, capitalize the first letter of each word in the identifier. \
Do not use underscores to differentiate words, or for that matter, anywhere in identifiers. \
There are two appropriate ways to capitalize identifiers, depending on the use of the identifier: 
- PascalCasing
- camelCasing

The PascalCasing convention, capitalizes the first character of each word (including acronyms over \
 two letters in length), as shown in the following examples: 

`PropertyDescriptor HtmlTag` 

A special case is made for two-letter acronyms in which both letters are capitalized, as shown in the \
following identifier: 

`IOStream`

The camelCasing convention, used for parameters, fields, and variables, capitalizes the first character of each word \
except the first word, as shown in the following examples. As the example also shows, two-letter acronyms \
that begin a camel-cased identifier are both lowercase. 

`propertyDescriptor ioStream htmlTag`

We will be using ==Allman Indentation Style== *(tabbing not represented in table)*



| Identifier    | Casing        | Example                                
|---------------|---------------|----------------------------------------
| Namespace     | Pascal        | `namespace System.Security`            
|               |               | `{`                                    
|               |               | `}`                                    
| Type          | Pascal        | `public class StreamReader`            
|               |               | `{`                                    
|               |               | `}`                                    
| Interface     | Pascal        | `public interface IEnumerable` 
|               |               | `{` 
|               |               | `}`
| Method        | Pascal        | `public class Object` 
|               |               | `{` 
|               |               | `public virtual string ToString();`
|               |               | `}`
| Property      | Camel         | `public class String` 
|               |               | `{` 
|               |               | `public int length { get; }` 
|               |               | `}`
| Event         | Pascal        | `public class String` 
|               |               | `{` 
|               |               | `public event EventHandler Exited;` 
|               |               | `}`
| Field         | Camel         | `public class MessageQueue` 
|               |               | `{` 
|               |               | `public static readonly timeSpan`
|               |               | `InfiniteTimeout;` 
|               |               | `}` 
|               |               | `public struct uInt32` 
|               |               | `{` 
|               |               | `public const min = 0;` 
|               |               | `}`
| Enum value    | Pascal        | `public enum FileMode` 
|               |               | `{` 
|               |               | `Append,`
|               |               | `...` 
|               |               | `}`
| Parameter     | Camel         | `public class Convert` 
|               |               | `{` 
|               |               | `public static int ToInt32(string value);` 
|               |               | `}`