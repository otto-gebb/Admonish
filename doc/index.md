# Admonish

A simple validation library for app services and domain entities.

[Github repository](https://github.com/otto-gebb/Admonish)

## Motivation

Sometimes validation logic is duplicated

- in application services, where a special kind of exception is
  thrown that is converted to a 400 HTTP response,
- and in domain entities, where preconditions are checked according to the usual OOP practice to
  keep the class state consistent.
  An ArgumentException or a sublcass of it is usually thrown in this case.

The idea behind this library is to throw that "special kind of exception" from entities without
referring to it explicitly, allowing one to remove duplication and keep entity-related validation
only in entities.

## Highlights

### Focus on primitive values

Because in domain entities one doesn't usually have access to DTOs Admonish is focused on
validating privitive values, not DTOs, unlike most other libraries.

Although some methods are provided to validate complex objects, such usage is discouraged.

### Error accumulation

Admonish accumulates errors until <xref:Admonish.ValidationResult.ThrowIfInvalid> is called. This
is in contrast to the approach taken in some other libraries that throw an exception
immediately on the first error encounterd.

### Extensibility

Users can write their own extension methods to the `ValidationResult` class to create custom named
validation rules, e.g. `CheckCreditCard`.

### Customization

Users can substitute the type of exception thrown on validation errors (see the
[UnsafeConfigureException](xref:Admonish.Validator.UnsafeConfigureException(System.Func{Admonish.ValidationResult,System.Exception}))
method) making domain validation code throw a custom exception (defined e.g. in a web
infrastructure library) without "knowing" about it.

### Simplicity

The code and the API of the library are kept as simple as possible.
