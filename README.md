# Kata Supermarket in Property-Based Testing

An implementation of the original [Kata Supermarket][kata-supermarket]
by  by @jesuswasrasta using 3 different approaches toward
Property-Based Testing:

* Using FsCheck.
* Using Hedgehog.
* Using ordinary xUnit tests, in a Property-Based Testing flavor.


[kata-supermarket]: https://github.com/jesuswasrasta/KataSupermarket-workshop

# Run tests
Run:

```bash
dotnet test
```


# Generation
```bash
dotnet new sln --name Supermarket
mkdir src

# WithFsCheck
cd src
dotnet new xunit --name WithFsCheck
dotnet add .\WithFsCheck\WithFsCheck.csproj package fscheck.xunit
cd ..
dotnet sln ./Supermarket.sln add ./src/WithFsCheck/WithFsCheck.csproj
```


# Requirements

* [English](docs/english/Requirements.md)
* Italiano
