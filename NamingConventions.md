# Capitalization Rules for Identifiers

We'll be using three main casing conventions with our identifiers depending on their use:
- UPPER_SNAKE_CASING
- PascalCasing
- camelCasing

See below for definitions of these specific conventions.

_______________
## Table of Casing Conventions by Identifier

| Identifier      | Casing        | Example                                
|-----------------|---------------|----------------------------------------
| Namespace       | Pascal        | `namespace System.Security`
| Type            | Pascal        | `public class StreamReader`
| Interface       | Pascal        | `public interface IEnumerable`
| Method          | Pascal        | `public virtual string ToString();`
| Property        | Pascal        | `public int Length { get; }`
| Event           | Pascal        | `public event EventHandler Exited;`
| Field           | Camel         | `public readonly string firstName;`
| Static Readonly | UPPER_SNAKE   | `public static readonly TimeSpan INFINITE_TIMEOUT;`
| Constants       | UPPER_SNAKE   | `public const MIN = 0;`
| Enum value      | Pascal        | `public enum FileMode { Append ... }`
| Parameter       | Camel         | `public static int ToInt32(string value);`

_______________
## Windows Forms Prefixes

| Control Type     | Prefix        | Example
|------------------|---------------|----------------------------------------
| Form class       | Frm           | `FrmLogin`
| Form variable    | frm           | `frmLogin`
| Button variable  | btn           | `btnBackButton`
| TextBox variable | txt           | `txtUserInput`
| Label variable   | lbl           | `lblErrorMessage`

_______________
## Casing Convention Definitions

### UPPER_SNAKE

The UPPER_SNAKE_CASING convention capitalizes each letter of each word and separates words by \
underscores, as shown in the following identifier:

`MAXIMUM_NUMBER_OF_CONNECTIONS`

### Pascal

The PascalCasing convention capitalizes the first character of each word (including acronyms over \
two letters in length), as shown in the following example:

`PropertyDescriptor HtmlTag`

A special case is made for two-letter acronyms in which both letters are capitalized, as shown in the \
following identifier:

`IOStream`

### camel

The camelCasing convention capitalizes the first character of each word except the first word, \
as shown in the following examples:

`PropertyDescriptor htmlTag`

`NetworkStream ioStream`

Note that acronyms that begin a camel-cased identifier are lowercase.
_______________
*For conventions that appear to be missing from this document, please reference Microsoft's suggestions for C# conventions*
