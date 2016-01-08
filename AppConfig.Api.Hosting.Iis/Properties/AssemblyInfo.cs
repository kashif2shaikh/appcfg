using System.Reflection;
using System.Runtime.InteropServices;
using AppConfig.Api;
using Microsoft.Owin;


[assembly: AssemblyTitle("AppConfig.Api.Hosting.Iis")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AppConfig.Api.Hosting.Iis")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: OwinStartup(typeof(Startup))]