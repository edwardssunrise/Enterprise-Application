## Utilities

Helper methods are defined in this context.

```csharp
///<summary>CrytoManager</summary>
string Base64Encrypt(string data);
string Base64Decrypt(string data);

///<summary>ImageManager</summary>
byte[] ImageToByteArray(System.Drawing.Image inputImage, string contentType);
Image ByteArrayToImage(byte[] inputByteArray);
ImageFormat ContentTypeToImageFormat(string contentType);
byte[] ConvertToSize(byte[] image, int? width, int? height);

///<summary>HelperExtensions</summary>
Boolean IsBase64String(this String value);

///<summary>IdentityManager</summary>
string[] IsIdentityNumber(string id);

///<summary>StringManager</summary>
string ToSlug(string incomingText);
```