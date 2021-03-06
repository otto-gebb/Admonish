# Introduction

How to validate parameters of entity methods:

[!code-csharp[Startup](~/../src/Sample/Domain/Entity.cs?highlight=12-19)]

If you need additional validation in an app service, for example, you need to check if such an
entity already exists in the database, you do it like this:

[!code-csharp[Startup](~/../src/Sample/WebApplication/AppService.cs?range=12-35&highlight=8-14)]

Imagine you have a custom validation exception (defined e.g. in your `WebApiUtils` library)
which you handle with a special middleware.
You can configure Admonish to throw the needed excepton type, so that validation errors from
domain and application modules are still handled by your middleware
without making any changes in it:

[!code-csharp[Startup](~/../src/Sample/WebApplication/Startup.cs?start=13&end=57&highlight=15-18)]

The middleware code could look like this.
[!code-csharp[Startup](~/../src/Sample/WebApiUtils/ErrorHandlerMiddleware.cs?start=10&end=47)]
