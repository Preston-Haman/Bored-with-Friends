# Capitalization Rules for Identifiers

To differentiate words in an identifier, capitalize the first letter of each word in the identifier. \
There are two appropriate ways to capitalize identifiers, depending on the use of the identifier: 
- PascalCasing
- camelCasing

The PascalCasing convention capitalizes the first character of each word (including acronyms over \
 two letters in length), as shown in the following examples: 

`PropertyDescriptor HtmlTag` 

A special case is made for two-letter acronyms in which both letters are capitalized, as shown in the \
following identifier: 

`IOStream`

The camelCasing convention, used for parameters, fields, and variables, capitalizes the first character of each word \
except the first word, as shown in the following examples. As the example also shows, two-letter acronyms \
that begin a camel-cased identifier are both lowercase. 

`propertyDescriptor ioStream htmlTag`

We will be using **Allman Indentation Style** *(tabbing not represented in table)*



| Identifier    | Casing        | Example                                
|---------------|---------------|----------------------------------------
| Namespace     | Pascal        | `namespace System.Security`                                               
| Type          | Pascal        | `public class StreamReader`                                               
| Interface     | Pascal        | `public interface IEnumerable` 
| Method        | Pascal        | `public virtual string ToString();`
| Property      | Pascal        | `public int Length { get; }`  
| Event         | Pascal        | `public event EventHandler Exited;`
| Field         | Camel         | `public string firstName;` 
|Static readonly| UPPER_SNAKE   | `public static readonly TimeSpan INFINITE_TIMEOUT;`
| Constants     | UPPER_SNAKE   | `public const MIN = 0;` 
| Enum value    | Pascal        | `public enum FileMode { Append, 88`
| Parameter     | Camel         | `public static int ToInt32(string value);` 

## Windows Forms

| Identifier    | Casing        | Example                                
|---------------|---------------|----------------------------------------
| Form classes  | Pascal        | `FrmLoginForm`                                               
| Buttons       | Camel         | `btnBackButton`                                               
| Text boxes    | Camel         | `txtUserInput`