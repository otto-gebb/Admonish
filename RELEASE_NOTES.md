### 1.0.2 - 2023-03-02

- Mark `NonNull*` methods with the `[NotNull]` attribute. This asserts to the compiler that the specified value
  cannot be null after calling the method, but the developer must not forget to call `ThrowIfInvalid` for the
  assertion to be true.
- Switch TargetFramework: `netstandard2.0` -> `netstandard2.1`.

### 1.0.1 - 2020-03-01

- Minor documentation improvemets.

### 1.0.0 - 2020-02-28

- Add optional custom error message to all validation helpers.
- Add new methods:
  - Null
  - Equal
  - NotEqual


### 0.3.0 - 2019-09-25

- Add `IsDefined<TEnum>` helper.

### 0.2.2 – 2019-03-10

- Expose the constructor of `ValidationResult`.

### 0.2.1 – 2018-12-18

- Add some date validation helpers.

### 0.1.9 – 2018-08-29

- Add some numeric validation helpers.

### 0.1.8 – 2018-08-29

- Add some helpers to validate string values.

### 0.1.7 – 2018-08-26

- Initial release.