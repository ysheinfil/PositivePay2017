# PositivePay2017
Cash Management

## Projects in Solution
### BankFileLibrary
  - VB.NET
  - Contains classes for bank records.
    - A generic class/interface that has data fields that apply to all banks.
    - Specific class for individual banks that may have aliases or additional data fields.
  - Contains classes for facilities. Since multiple facilities may use the same bank, the facility class goes under the bank's name.
    - A generic class/interface that has data fields and functions/methods that apply to all facilities.
    - One of the methods that are necessary is creating the file that the bank is expecting. It needs to be in a certain format. Makes use of functions that are defined in a Utilities class.
    
### BankFileTestProject
  - C#.NET
  - Uses MSTest Framework
  - Tests for BankRecord, Facility and Utilities
### ConsoleAppBench
  - VB.NET
  - Intended as a workbench for experimenting features while working on them.
### PositivePayMonitor
  - C#.NET
### PositivePayMonitorService
  - C#.NET

## Third Party Software
### Log4Net
