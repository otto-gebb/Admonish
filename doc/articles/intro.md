# Introduction

How to validate parameters of entity methods:

[!code-csharp[Startup](~/../src/Sample/Domain/Entity.cs?highlight=13-20)]

If you need additional validation in an app service, for example, you need to check if such an
entity already exists in the database, you do it like this:

[!code-csharp[Startup](~/../src/Sample/WebApplication/AppService.cs?range=12-29&highlight=19-24)]

Imagine you have a custom validatin exception (defined e.g. in your `WebApiUtils` library)
which you handle with a special middleware

[!code-csharp[Startup](~/../src/Sample/WebApiUtils/ErrorHandlerMiddleware.cs?start=12&end=45)]

You can configure Admonish to throw the needed excepton type, so that validation errors from
domain and application modules are still handled by your middleware
without making any changes in it:

[!code-csharp[Startup](~/../src/Sample/WebApplication/Startup.cs?start=17&end=54)]
