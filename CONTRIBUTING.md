<!-- omit in toc -->
# Contributing to iiwi

First off, thanks for taking the time to contribute! ❤️

All types of contributions are encouraged and valued. See the [Table of Contents](#table-of-contents) for different ways to help and details about how this project handles them. Please make sure to read the relevant section before making your contribution. It will make it a lot easier for us maintainers and smooth out the experience for all involved. The community looks forward to your contributions. 🎉

> And if you like the project, but just don't have time to contribute, that's fine. There are other easy ways to support the project and show your appreciation, which we would also be very happy about:
> - Star the project
> - Tweet about it
> - Refer this project in your project's readme
> - Mention the project at local meetups and tell your friends/colleagues

<!-- omit in toc -->
## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [I Have a Question](#i-have-a-question)
  - [I Want To Contribute](#i-want-to-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Enhancements](#suggesting-enhancements)
  - [Your First Code Contribution](#your-first-code-contribution)
  - [Improving The Documentation](#improving-the-documentation)
- [Styleguides](#styleguides)
  - [Coding Standards](#coding-standards)
  - [Commit Messages](#commit-messages)
- [Join The Project Team](#join-the-project-team)


## Code of Conduct

This project and everyone participating in it is governed by the
[iiwi Code of Conduct](CODE_OF_CONDUCT.md).
By participating, you are expected to uphold this code. Please report unacceptable behavior
to <conduct@iiwi.dev>.


## I Have a Question

> If you want to ask a question, we assume that you have read the available [Documentation]().

Before you ask a question, it is best to search for existing [Issues](https://github.com/itsarisid/iiwi/issues) that might help you. In case you have found a suitable issue and still need clarification, you can write your question in this issue. It is also advisable to search the internet for answers first.

If you then still feel the need to ask a question and need clarification, we recommend the following:

- Open an [Issue](https://github.com/itsarisid/iiwi/issues/new).
- Provide as much context as you can about what you're running into.
- Provide project and platform versions (dotnet, etc), depending on what seems relevant.

We will then take care of the issue as soon as possible.

## I Want To Contribute

> ### Legal Notice <!-- omit in toc -->
> When contributing to this project, you must agree that you have authored 100% of the content, that you have the necessary rights to the content and that the content you contribute may be provided under the project licence.

### Reporting Bugs

<!-- omit in toc -->
#### Before Submitting a Bug Report

A good bug report shouldn't leave others needing to chase you up for more information. Therefore, we ask you to investigate carefully, collect information and describe the issue in detail in your report. Please complete the following steps in advance to help us fix any potential bug as fast as possible.

- Make sure that you are using the latest version.
- Determine if your bug is really a bug and not an error on your side e.g. using incompatible environment components/versions.
- To see if other users have experienced (and potentially already solved) the same issue you are having, check if there is not already a bug report existing for your bug or error in the [bug tracker](https://github.com/itsarisid/iiwi/issues?q=label%3Abug).
- Collect information about the bug:
  - Stack trace (Traceback)
  - OS, Platform and Version (Windows, Linux, macOS)
  - Version of the .NET SDK and runtime.
  - Possibly your input and the output
  - Can you reliably reproduce the issue? And can you also reproduce it with older versions?

<!-- omit in toc -->
#### How Do I Submit a Good Bug Report?

> You must never report security related issues, vulnerabilities or bugs including sensitive information to the issue tracker, or elsewhere in public. Instead sensitive bugs must be sent by email to <security@iiwi.dev>.

We use GitHub issues to track bugs and errors. If you run into an issue with the project:

- Open an [Issue](https://github.com/itsarisid/iiwi/issues/new).
- Explain the behavior you would expect and the actual behavior.
- Please provide as much context as possible and describe the *reproduction steps* that someone else can follow to recreate the issue on their own. This usually includes your code. For good bug reports you should isolate the problem and create a reduced test case.
- Provide the information you collected in the previous section.

### Suggesting Enhancements

This section guides you through submitting an enhancement suggestion for iiwi, **including completely new features and minor improvements to existing functionality**. Following these guidelines will help maintainers and the community to understand your suggestion and find related suggestions.

<!-- omit in toc -->
#### Before Submitting an Enhancement

- Make sure that you are using the latest version.
- Read the [documentation]() carefully and find out if the functionality is already covered.
- Perform a [search](https://github.com/itsarisid/iiwi/issues) to see if the enhancement has already been suggested. If it has, add a comment to the existing issue instead of opening a new one.

<!-- omit in toc -->
#### How Do I Submit a Good Enhancement Suggestion?

Enhancement suggestions are tracked as [GitHub issues](https://github.com/itsarisid/iiwi/issues).

- Use a **clear and descriptive title** for the issue to identify the suggestion.
- Provide a **step-by-step description of the suggested enhancement** in as many details as possible.
- **Describe the current behavior** and **explain which behavior you expected to see instead** and why.
- **Explain why this enhancement would be useful** to most iiwi users.

### Your First Code Contribution

1. Fork the repository.
2. Clone your fork locally.
3. Create a new branch for your feature or fix.
4. Make your changes, adhering to the [Styleguides](#styleguides).
5. Run tests and verify the build using `dotnet build iiwi.slnx`.
6. Commit your changes with clear messages.
7. Push to your fork and submit a Pull Request.

### Improving The Documentation

Documentation is key to the success of iiwi. If you find typos, missing sections, or unclear explanations, please contribute!
- XML documentation in C# files is mandatory for all public members.
- Markdown files should be formatted correctly.

## Styleguides

### Coding Standards

To maintain a high-quality codebase, please adhere to the following standards:

1.  **XML Documentation**: All public classes, interfaces, methods, properties, and constructors MUST have XML documentation comments (`/// <summary>...`).
    -   Use `<param>` tags for method parameters.
    -   Use `<returns>` tags for return values.
    -   Use `<exception>` tags for thrown exceptions.
2.  **Namespace Cleanup**: Remove all unused `using` directives. The code should be clean and free of unnecessary imports.
3.  **Naming Conventions**: Follow standard C# naming conventions (PascalCase for classes/methods, camelCase for local variables/parameters).
4.  **Code Formatting**: Use standard .NET code formatting settings (default Visual Studio settings).
5.  **Build Verification**: Ensure the solution builds successfully (`dotnet build iiwi.slnx`) before submitting a PR.

### Commit Messages

- Use clear and descriptive commit messages.
- Start with a verb (e.g., "Add", "Fix", "Update", "Remove").
- Reference issue numbers if applicable.

## Join The Project Team

If you are interested in becoming a maintainer, please reach out to the project owners.

<!-- omit in toc -->
## Attribution
This guide is based on the [contributing.md](https://contributing.md/generator)!
