# Admonish - Suggested Improvements and Features

This document outlines potential improvements and new features for the Admonish validation library. Suggestions are organized by category and prioritized.

## Table of Contents
1. [New Validation Methods](#1-new-validation-methods)
2. [Core Framework Enhancements](#2-core-framework-enhancements)
3. [Developer Experience Improvements](#3-developer-experience-improvements)
4. [Performance Optimizations](#4-performance-optimizations)
5. [Modern .NET Features](#5-modern-net-features)
6. [Documentation and Examples](#6-documentation-and-examples)
7. [Testing Enhancements](#7-testing-enhancements)
8. [Integration with Other Frameworks](#8-integration-with-other-frameworks)

---

## 1. New Validation Methods

### 1.1 String Validators - Common Patterns

**Priority: HIGH** - These are frequently requested validation patterns.

```csharp
// Email validation
public static ValidationResult Email(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

// URL validation
public static ValidationResult Url(
    this ValidationResult r,
    string key,
    string? value,
    bool requireHttps = false,
    string? message = null)

// Phone number validation (with region support)
public static ValidationResult PhoneNumber(
    this ValidationResult r,
    string key,
    string? value,
    string? region = null,
    string? message = null)

// Credit card validation (Luhn algorithm)
public static ValidationResult CreditCard(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

// IPv4 validation
public static ValidationResult IPv4(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

// IPv6 validation
public static ValidationResult IPv6(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

// GUID/UUID validation
public static ValidationResult Guid(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

// Alphanumeric validation
public static ValidationResult Alphanumeric(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

// MinLength and MaxLength (separate from LengthBetween)
public static ValidationResult MinLength(
    this ValidationResult r,
    string key,
    string? value,
    int minLength,
    string? message = null)

public static ValidationResult MaxLength(
    this ValidationResult r,
    string key,
    string? value,
    int maxLength,
    string? message = null)

// ExactLength
public static ValidationResult ExactLength(
    this ValidationResult r,
    string key,
    string? value,
    int length,
    string? message = null)

// StartsWith/EndsWith
public static ValidationResult StartsWith(
    this ValidationResult r,
    string key,
    string? value,
    string prefix,
    StringComparison comparison = StringComparison.Ordinal,
    string? message = null)

public static ValidationResult EndsWith(
    this ValidationResult r,
    string key,
    string? value,
    string suffix,
    StringComparison comparison = StringComparison.Ordinal,
    string? message = null)

// Contains
public static ValidationResult Contains(
    this ValidationResult r,
    string key,
    string? value,
    string substring,
    StringComparison comparison = StringComparison.Ordinal,
    string? message = null)

// NotContains
public static ValidationResult NotContains(
    this ValidationResult r,
    string key,
    string? value,
    string substring,
    StringComparison comparison = StringComparison.Ordinal,
    string? message = null)

// Case validation
public static ValidationResult Uppercase(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)

public static ValidationResult Lowercase(
    this ValidationResult r,
    string key,
    string? value,
    string? message = null)
```

### 1.2 Numeric Validators - Extended Support

**Priority: MEDIUM** - Support for more numeric types.

```csharp
// Support for long, ulong, short, ushort, byte, sbyte, float, double
// Current library only supports int and decimal

// Example for long:
public static ValidationResult Min(
    this ValidationResult r,
    string key,
    long value,
    long minValue,
    string? message = null)

public static ValidationResult Max(
    this ValidationResult r,
    string key,
    long value,
    long maxValue,
    string? message = null)

public static ValidationResult Between(
    this ValidationResult r,
    string key,
    long value,
    long minValue,
    long maxValue,
    string? message = null)

// Positive/Negative checks
public static ValidationResult Positive<T>(
    this ValidationResult r,
    string key,
    T value,
    string? message = null) where T : IComparable<T>

public static ValidationResult Negative<T>(
    this ValidationResult r,
    string key,
    T value,
    string? message = null) where T : IComparable<T>

// Zero check
public static ValidationResult NotZero<T>(
    this ValidationResult r,
    string key,
    T value,
    string? message = null) where T : IComparable<T>

// Precision validation for decimal
public static ValidationResult Precision(
    this ValidationResult r,
    string key,
    decimal value,
    int maxPrecision,
    int maxScale,
    string? message = null)
```

### 1.3 Collection Validators

**Priority: HIGH** - Collections are common validation scenarios.

```csharp
// Not empty
public static ValidationResult NotEmpty<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    string? message = null)

// Minimum/maximum count
public static ValidationResult MinCount<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    int minCount,
    string? message = null)

public static ValidationResult MaxCount<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    int maxCount,
    string? message = null)

public static ValidationResult CountBetween<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    int minCount,
    int maxCount,
    string? message = null)

// Exact count
public static ValidationResult ExactCount<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    int count,
    string? message = null)

// Contains/NotContains specific item
public static ValidationResult Contains<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    T item,
    string? message = null)

public static ValidationResult NotContains<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    T item,
    string? message = null)

// All items meet condition
public static ValidationResult All<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    Func<T, bool> predicate,
    string? message = null)

// Any item meets condition
public static ValidationResult Any<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    Func<T, bool> predicate,
    string? message = null)

// Unique items (no duplicates)
public static ValidationResult Unique<T>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    string? message = null)

public static ValidationResult UniqueBy<T, TKey>(
    this ValidationResult r,
    string key,
    IEnumerable<T>? value,
    Func<T, TKey> keySelector,
    string? message = null)
```

### 1.4 DateTime Extended Validators

**Priority: MEDIUM** - Additional time-based validation.

```csharp
// Is in the past/future
public static ValidationResult InPast(
    this ValidationResult r,
    string key,
    DateTime value,
    string? message = null)

public static ValidationResult InFuture(
    this ValidationResult r,
    string key,
    DateTime value,
    string? message = null)

// Age validation (for birth dates)
public static ValidationResult MinAge(
    this ValidationResult r,
    string key,
    DateTime birthDate,
    int minAge,
    string? message = null)

public static ValidationResult MaxAge(
    this ValidationResult r,
    string key,
    DateTime birthDate,
    int maxAge,
    string? message = null)

// Time range from now
public static ValidationResult WithinTimeSpan(
    this ValidationResult r,
    string key,
    DateTime value,
    TimeSpan maxDifference,
    string? message = null)

// Date only (ignore time)
public static ValidationResult DateOnly(
    this ValidationResult r,
    string key,
    DateTime value,
    string? message = null)

// Specific day of week
public static ValidationResult DayOfWeek(
    this ValidationResult r,
    string key,
    DateTime value,
    DayOfWeek expectedDayOfWeek,
    string? message = null)
```

### 1.5 File/Path Validators

**Priority: LOW** - Useful for file upload scenarios.

```csharp
// File extension validation
public static ValidationResult FileExtension(
    this ValidationResult r,
    string key,
    string? filePath,
    string[] allowedExtensions,
    string? message = null)

// File size validation (requires file path or Stream)
public static ValidationResult MaxFileSize(
    this ValidationResult r,
    string key,
    string? filePath,
    long maxSizeInBytes,
    string? message = null)

// Valid path
public static ValidationResult ValidPath(
    this ValidationResult r,
    string key,
    string? path,
    string? message = null)

// File exists
public static ValidationResult FileExists(
    this ValidationResult r,
    string key,
    string? filePath,
    string? message = null)

// Directory exists
public static ValidationResult DirectoryExists(
    this ValidationResult r,
    string key,
    string? directoryPath,
    string? message = null)
```

---

## 2. Core Framework Enhancements

### 2.1 Conditional Validation

**Priority: HIGH** - Very common pattern in validation.

```csharp
// When/Unless - conditional validation
public static ValidationResult When(
    this ValidationResult r,
    bool condition,
    Func<ValidationResult, ValidationResult> validate)
{
    if (condition)
    {
        return validate(r);
    }
    return r;
}

public static ValidationResult Unless(
    this ValidationResult r,
    bool condition,
    Func<ValidationResult, ValidationResult> validate)
{
    return r.When(!condition, validate);
}

// Example usage:
Validator.Create()
    .When(user.IsAdmin, r => r
        .NonNullOrEmpty(nameof(user.AdminCode), user.AdminCode))
    .Unless(user.IsGuest, r => r
        .NonNullOrEmpty(nameof(user.Email), user.Email))
    .ThrowIfInvalid();
```

### 2.2 Async Validation Support

**Priority: MEDIUM** - Required for database/external API validations.

```csharp
// New class: AsyncValidationResult
public class AsyncValidationResult
{
    // Similar to ValidationResult but returns Task<ValidationResult>

    public async Task<ValidationResult> MustAsync(
        string key,
        Func<Task<bool>> asyncPredicate,
        string message)
    {
        if (!await asyncPredicate())
        {
            _result.AddError(key, message);
        }
        return _result;
    }

    public async Task<ValidationResult> ThrowIfInvalidAsync()
    {
        if (!_result.Success)
        {
            throw _exceptionFactory(_result);
        }
        return _result;
    }
}

// Example usage:
await Validator.CreateAsync()
    .MustAsync(
        nameof(user.Email),
        async () => !await _db.Users.AnyAsync(u => u.Email == user.Email),
        "Email already exists")
    .ThrowIfInvalidAsync();
```

### 2.3 Validation Context

**Priority: MEDIUM** - Pass contextual data through validation chain.

```csharp
public class ValidationContext
{
    private readonly Dictionary<string, object> _data = new();

    public void Set<T>(string key, T value)
    {
        _data[key] = value;
    }

    public T Get<T>(string key)
    {
        return (T)_data[key];
    }

    public bool TryGet<T>(string key, out T value)
    {
        // Implementation
    }
}

// Add to ValidationResult
public ValidationContext Context { get; } = new ValidationContext();

// Example usage:
var result = Validator.Create();
result.Context.Set("CurrentUser", currentUser);
result.Context.Set("RequestTime", DateTime.UtcNow);

// Custom validators can access context
result.Check(
    nameof(order.CreatedBy),
    order.CreatedBy == result.Context.Get<User>("CurrentUser").Id,
    "Order must be created by current user");
```

### 2.4 Custom Error Severity Levels

**Priority: LOW** - Support warnings, info, etc.

```csharp
public enum ValidationSeverity
{
    Error,
    Warning,
    Info
}

// Modify ValidationResult to track severity
public class ValidationError
{
    public string Key { get; set; }
    public string Message { get; set; }
    public ValidationSeverity Severity { get; set; }
}

// Add severity parameter to validators
public static ValidationResult AddError(
    this ValidationResult r,
    string key,
    string message,
    ValidationSeverity severity = ValidationSeverity.Error)
```

### 2.5 Fluent Error Message Formatting

**Priority: MEDIUM** - Better error message customization.

```csharp
// WithMessage - override default message
public static ValidationResult WithMessage(
    this ValidationResult r,
    Func<ValidationResult, string> messageFactory)

// Example:
Validator.Create()
    .Min(nameof(age), age, 18)
    .WithMessage(r => $"Age must be at least 18, but was {age}")
    .ThrowIfInvalid();

// Or use placeholders
public static ValidationResult Min(
    this ValidationResult r,
    string key,
    int value,
    int minValue,
    string? message = null)
{
    message ??= "The value must be greater than or equal to {MinValue} but was {ActualValue}.";
    message = message
        .Replace("{MinValue}", minValue.ToString())
        .Replace("{ActualValue}", value.ToString())
        .Replace("{Key}", key);
    // ...
}
```

### 2.6 Validation Result Combination

**Priority: LOW** - Combine multiple validation results.

```csharp
// Merge multiple validation results
public static ValidationResult Merge(
    this ValidationResult r,
    ValidationResult other)
{
    foreach (var error in other.ToDictionary())
    {
        foreach (var message in error.Value)
        {
            r.AddError(error.Key, message);
        }
    }
    return r;
}

// Example:
var result1 = ValidateAddress(address);
var result2 = ValidatePayment(payment);
var combined = Validator.Create()
    .Merge(result1)
    .Merge(result2);
```

### 2.7 Stop on First Error Option

**Priority: LOW** - For performance-sensitive scenarios.

```csharp
// Add FailFast mode
public class ValidationOptions
{
    public bool FailFast { get; set; } = false;
}

public ValidationResult(
    Func<ValidationResult, Exception> exceptionFactory,
    ValidationOptions? options = null)

// When FailFast is true, throw immediately on first error
```

---

## 3. Developer Experience Improvements

### 3.1 Better IntelliSense Support

**Priority: HIGH** - Add more XML documentation examples.

```csharp
/// <summary>
/// Ensures that the specified string is not null or empty.
/// </summary>
/// <param name="r">The validation result.</param>
/// <param name="key">The key to associate the error with.</param>
/// <param name="value">The string to check.</param>
/// <param name="message">An optional error message.</param>
/// <returns>The validation result.</returns>
/// <example>
/// <code>
/// Validator.Create()
///     .NonNullOrEmpty(nameof(user.Name), user.Name)
///     .ThrowIfInvalid();
/// </code>
/// </example>
/// <exception cref="ValidationException">
/// Thrown when validation fails and ThrowIfInvalid() is called.
/// </exception>
public static ValidationResult NonNullOrEmpty(...)
```

### 3.2 Validation Rule Builder

**Priority: MEDIUM** - Reusable validation rule definitions.

```csharp
// Define reusable rules
public class ValidationRule<T>
{
    private readonly Func<ValidationResult, T, ValidationResult> _validator;

    public ValidationRule(Func<ValidationResult, T, ValidationResult> validator)
    {
        _validator = validator;
    }

    public ValidationResult Apply(ValidationResult r, T value)
    {
        return _validator(r, value);
    }
}

// Example: Define common rules
public static class CommonRules
{
    public static readonly ValidationRule<string> EmailRule =
        new ValidationRule<string>((r, email) =>
            r.Email(nameof(email), email));

    public static readonly ValidationRule<string> StrongPasswordRule =
        new ValidationRule<string>((r, password) => r
            .MinLength(nameof(password), password, 8)
            .Matches(nameof(password), password,
                new Regex(@"[A-Z]"), "Must contain uppercase")
            .Matches(nameof(password), password,
                new Regex(@"[0-9]"), "Must contain digit"));
}

// Usage:
Validator.Create()
    .ApplyRule(CommonRules.EmailRule, user.Email)
    .ApplyRule(CommonRules.StrongPasswordRule, user.Password)
    .ThrowIfInvalid();
```

### 3.3 Validation Profiles

**Priority: LOW** - Group related validations.

```csharp
public abstract class ValidationProfile<T>
{
    protected abstract void Configure(ValidationResult r, T entity);

    public ValidationResult Validate(T entity)
    {
        var result = Validator.Create();
        Configure(result, entity);
        return result;
    }
}

// Example:
public class UserValidationProfile : ValidationProfile<User>
{
    protected override void Configure(ValidationResult r, User user)
    {
        r.NonNullOrEmpty(nameof(user.Name), user.Name)
         .Email(nameof(user.Email), user.Email)
         .Min(nameof(user.Age), user.Age, 18);
    }
}

// Usage:
var profile = new UserValidationProfile();
profile.Validate(user).ThrowIfInvalid();
```

### 3.4 Validation Attributes (Optional)

**Priority: LOW** - Declarative validation style.

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ValidateAttribute : Attribute
{
    public string? Message { get; set; }
}

public class NonNullOrEmptyAttribute : ValidateAttribute { }

public class MinAttribute : ValidateAttribute
{
    public int Value { get; }
    public MinAttribute(int value) => Value = value;
}

// Example:
public class User
{
    [NonNullOrEmpty(Message = "Name is required")]
    public string Name { get; set; }

    [Min(18, Message = "Must be at least 18")]
    public int Age { get; set; }
}

// Validator reflection helper:
public static ValidationResult ValidateObject<T>(
    this ValidationResult r,
    T obj)
{
    // Use reflection to read attributes and apply validations
}
```

---

## 4. Performance Optimizations

### 4.1 Lazy Error Message Construction

**Priority: MEDIUM** - Avoid string formatting when validation passes.

```csharp
// Instead of:
public static ValidationResult Min(
    this ValidationResult r,
    string key,
    int value,
    int minValue,
    string? message = null)
{
    if (value < minValue)
    {
        message ??= $"The value must be greater than or equal to {minValue}.";
        r.AddError(key, message);
    }
    return r;
}

// Use lazy message:
public static ValidationResult Min(
    this ValidationResult r,
    string key,
    int value,
    int minValue,
    Func<string>? messageFactory = null)
{
    if (value < minValue)
    {
        var message = messageFactory?.Invoke()
            ?? $"The value must be greater than or equal to {minValue}.";
        r.AddError(key, message);
    }
    return r;
}
```

### 4.2 Compiled Regex for Common Patterns

**Priority: LOW** - Pre-compile regex patterns.

```csharp
public static class CommonPatterns
{
    public static readonly Regex Email =
        new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex Url =
        new Regex(@"^https?://[^\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
}
```

### 4.3 Object Pooling for ValidationResult

**Priority: LOW** - Reuse ValidationResult instances.

```csharp
// Use ArrayPool for error collections
private List<string> _errorList = ArrayPool<string>.Shared.Rent(initialSize);

// Implement IDisposable to return to pool
public void Dispose()
{
    if (_errorList != null)
    {
        ArrayPool<string>.Shared.Return(_errorList.ToArray());
    }
}
```

---

## 5. Modern .NET Features

### 5.1 Target Modern .NET Versions

**Priority: HIGH** - Support newer .NET versions.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <!-- Multi-target -->
    <TargetFrameworks>netstandard2.1;net6.0;net8.0</TargetFrameworks>

    <!-- Or just modern .NET -->
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>12.0</LangVersion>
  </PropertyGroup>
</Project>
```

Benefits:
- Better performance
- Modern C# features (records, pattern matching, etc.)
- Native AOT support
- Generic math (for numeric validators)

### 5.2 Use Generic Math (INumber<T>)

**Priority: MEDIUM** - Simplify numeric validators.

```csharp
// Instead of separate methods for int, decimal, long, etc.
// Use generic constraints (requires .NET 7+)
public static ValidationResult Min<T>(
    this ValidationResult r,
    string key,
    T value,
    T minValue,
    string? message = null) where T : INumber<T>, IComparable<T>
{
    if (value.CompareTo(minValue) < 0)
    {
        message ??= $"The value must be greater than or equal to {minValue}.";
        r.AddError(key, message);
    }
    return r;
}
```

### 5.3 Source Generators

**Priority: LOW** - Generate validation code at compile time.

```csharp
// Generate validation methods from interfaces
[GenerateValidator]
public partial class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Source generator creates:
public partial class User
{
    public static ValidationResult Validate(User user)
    {
        return Validator.Create()
            .NonNullOrEmpty(nameof(user.Name), user.Name)
            .Min(nameof(user.Age), user.Age, 0);
    }
}
```

### 5.4 Init-only Properties and Records

**Priority: MEDIUM** - Better immutability support.

```csharp
// ValidationError as a record
public record ValidationError(
    string Key,
    string Message,
    ValidationSeverity Severity = ValidationSeverity.Error);

// Immutable ValidationResult
public class ValidationResult
{
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Errors { get; init; }
}
```

### 5.5 Nullable Reference Type Improvements

**Priority: MEDIUM** - Better null handling.

```csharp
// Use nullable attributes more effectively
public static ValidationResult NonNullOrEmpty(
    this ValidationResult r,
    string key,
    [NotNullWhen(true)] string? value,
    [CallerArgumentExpression("value")] string? paramName = null,
    string? message = null)
```

---

## 6. Documentation and Examples

### 6.1 More Code Examples

**Priority: HIGH** - Add practical examples.

Topics to cover:
- ASP.NET Core integration (already partially covered)
- Entity Framework integration
- Blazor form validation
- Minimal APIs
- gRPC services
- Azure Functions
- Background services
- Complex object validation
- Cross-field validation patterns
- Custom validator extensions

### 6.2 Migration Guide

**Priority: MEDIUM** - From other validation libraries.

Create guides for:
- Migrating from FluentValidation
- Migrating from Data Annotations
- Migrating from custom validation code
- Comparison matrix (when to use Admonish vs alternatives)

### 6.3 Best Practices Guide

**Priority: MEDIUM** - Recommended patterns.

Topics:
- Domain-driven design with Admonish
- Error handling strategies
- Performance considerations
- Testing validation logic
- Localization approaches
- Custom validator creation
- Validation in layers (Domain vs Application vs Presentation)

### 6.4 Interactive Examples

**Priority: LOW** - Live playground.

- Create a Blazor WebAssembly playground
- Try.NET integration
- NuGet package README improvements

---

## 7. Testing Enhancements

### 7.1 Validation Testing Helpers

**Priority: MEDIUM** - Make testing easier.

```csharp
// Test helpers namespace
public static class ValidationTestExtensions
{
    public static ValidationResult ShouldBeValid(this ValidationResult r)
    {
        if (!r.Success)
        {
            throw new AssertionException(
                $"Expected valid result but got errors: {r}");
        }
        return r;
    }

    public static ValidationResult ShouldBeInvalid(this ValidationResult r)
    {
        if (r.Success)
        {
            throw new AssertionException(
                "Expected invalid result but validation succeeded");
        }
        return r;
    }

    public static ValidationResult ShouldHaveError(
        this ValidationResult r,
        string key,
        string? expectedMessage = null)
    {
        var errors = r.ToDictionary();
        if (!errors.ContainsKey(key))
        {
            throw new AssertionException(
                $"Expected error for key '{key}' but none found");
        }

        if (expectedMessage != null &&
            !errors[key].Any(m => m.Contains(expectedMessage)))
        {
            throw new AssertionException(
                $"Expected error message containing '{expectedMessage}' " +
                $"for key '{key}' but got: {string.Join(", ", errors[key])}");
        }

        return r;
    }
}

// Example test:
[Test]
public void User_WithInvalidAge_ShouldFailValidation()
{
    var result = Validator.Create()
        .Min(nameof(user.Age), -5, 0);

    result.ShouldBeInvalid()
          .ShouldHaveError(nameof(user.Age), "greater than or equal to 0");
}
```

### 7.2 Benchmark Suite

**Priority: LOW** - Performance benchmarking.

```csharp
// BenchmarkDotNet tests
[MemoryDiagnoser]
public class ValidationBenchmarks
{
    [Benchmark]
    public void SimpleValidation()
    {
        Validator.Create()
            .NonNullOrEmpty("name", "John")
            .Min("age", 25, 18)
            .ThrowIfInvalid();
    }

    [Benchmark]
    public void ComplexValidation()
    {
        // Benchmark complex scenarios
    }
}
```

---

## 8. Integration with Other Frameworks

### 8.1 ASP.NET Core Model Binding

**Priority: HIGH** - Seamless integration with MVC/API.

```csharp
// Custom model binder
public class ValidatedModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Bind model
        var model = // ... bind model

        // Validate using Admonish
        var result = ValidateModel(model);
        if (!result.Success)
        {
            foreach (var error in result.ToDictionary())
            {
                bindingContext.ModelState.AddModelError(
                    error.Key,
                    string.Join("; ", error.Value));
            }
        }
    }
}

// Or attribute-based:
public IActionResult CreateUser(
    [FromBody, ValidateWith(typeof(UserValidator))] User user)
{
    // Model is already validated
}
```

### 8.2 FluentValidation Interop

**Priority: MEDIUM** - Work alongside FluentValidation.

```csharp
// Convert FluentValidation rules to Admonish
public static ValidationResult FromFluentValidation<T>(
    this ValidationResult r,
    IValidator<T> fluentValidator,
    T instance)
{
    var fluentResult = fluentValidator.Validate(instance);
    foreach (var error in fluentResult.Errors)
    {
        r.AddError(error.PropertyName, error.ErrorMessage);
    }
    return r;
}
```

### 8.3 Data Annotations Interop

**Priority: MEDIUM** - Use with System.ComponentModel.DataAnnotations.

```csharp
// Validate using data annotations
public static ValidationResult ValidateDataAnnotations<T>(
    this ValidationResult r,
    T instance)
{
    var context = new ValidationContext(instance);
    var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

    if (!Validator.TryValidateObject(instance, context, results, true))
    {
        foreach (var result in results)
        {
            var key = result.MemberNames.FirstOrDefault()
                ?? ValidationResult.NoKey;
            r.AddError(key, result.ErrorMessage ?? "Validation failed");
        }
    }

    return r;
}
```

### 8.4 OpenAPI/Swagger Integration

**Priority: LOW** - Document validation rules in API schema.

```csharp
// Generate OpenAPI schema from Admonish validators
public class AdmonishSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Read validation rules and add to schema
        // e.g., MinLength -> minLength, MaxLength -> maxLength
    }
}
```

### 8.5 Localization Support

**Priority: MEDIUM** - Multi-language error messages.

```csharp
// Use IStringLocalizer
public static class LocalizedValidationExtensions
{
    public static ValidationResult NonNullOrEmpty(
        this ValidationResult r,
        string key,
        string? value,
        IStringLocalizer localizer,
        string? messageKey = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            var message = localizer[messageKey ?? "Validation.NonNullOrEmpty"];
            r.AddError(key, message);
        }
        return r;
    }
}

// Or global localizer configuration
Validator.UnsafeConfigureLocalizer(serviceProvider.GetRequiredService<IStringLocalizer>());
```

---

## Implementation Priority Summary

### Critical (Implement First)
1. ✅ Common string validators (Email, URL, Phone, etc.)
2. ✅ Collection validators (NotEmpty, MinCount, etc.)
3. ✅ Conditional validation (When/Unless)
4. ✅ Better documentation and examples
5. ✅ ASP.NET Core model binding integration

### High Priority
6. ✅ Async validation support
7. ✅ Extended numeric type support (long, float, etc.)
8. ✅ Additional DateTime validators
9. ✅ Target modern .NET versions (.NET 6/8)
10. ✅ Validation testing helpers

### Medium Priority
11. Generic math support (INumber<T>)
12. Validation context
13. Better error message formatting
14. Validation rule builder
15. Localization support
16. FluentValidation/DataAnnotations interop

### Low Priority
17. Custom error severity levels
18. Validation profiles
19. Stop on first error option
20. Source generators
21. File/Path validators
22. Performance optimizations (pooling, etc.)

---

## Breaking Changes to Consider

If creating version 3.0.0, consider:

1. **Target only .NET 6+** - Drop .NET Standard 2.1
2. **Use generic math** - Simplify numeric validators
3. **Immutable ValidationResult** - Use init-only properties
4. **Async-first API** - Make async the default
5. **Rename methods** - More consistent naming (e.g., `NotNullOrEmpty` → `Required`)
6. **Remove legacy patterns** - Clean up deprecated code

---

## Community Engagement

To gather feedback on these suggestions:

1. Create GitHub Discussions for each major category
2. Create a roadmap project board
3. Mark issues as "help wanted" for community contributions
4. Create issue templates for feature requests
5. Add contributor guidelines
6. Create a Discord/Slack for discussions

---

## Conclusion

This document outlines numerous potential improvements to Admonish. The library's current design is solid, and these enhancements would:

1. **Expand validation capabilities** - Cover more common use cases
2. **Improve developer experience** - Make the API more intuitive and discoverable
3. **Enhance performance** - Optimize for high-throughput scenarios
4. **Modernize the codebase** - Leverage latest .NET features
5. **Increase adoption** - Better documentation and integration examples

The priority system helps guide implementation order, focusing first on features that provide the most value to users.

Would you like me to start implementing any of these features? I recommend starting with the "Critical" priority items, particularly:
- Common string validators (email, URL, etc.)
- Collection validators
- Conditional validation (When/Unless)
