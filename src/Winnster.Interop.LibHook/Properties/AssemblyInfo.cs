//--------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Winnster Solutions, LLC">
//      Copyright (c) Winnster Solutions, LLC. All rights reserved.
// </copyright>
//--------------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Winnster.Interop.LibHook")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyProduct("libhook")]
[assembly: AssemblyCompany("Jeff Winn")]
[assembly: AssemblyCopyright("Copyright (c) Jeff Winn. All rights reserved.")]
[assembly: AssemblyCulture("")]

#if (DEBUG)
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("RETAIL")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("fa38ab62-a738-47e1-9926-8d39d47e6986")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
