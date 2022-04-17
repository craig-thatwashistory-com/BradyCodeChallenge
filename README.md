# BradyCodeChallenge
Submission for Brady Code Challenge - Craig McClelland

Brady Code Challenge Notes

Usage Notes
•	Requires a folder structure to be created, matching the configured paths in App.config
o	The current folders configured/required are
	C:\BradyTechTest\Input
	C:\BradyTechTest\Output
	C:\BradyTechTest\Archive
•	Takes an optional command line parameter for a path to the folder containing the reference data file. If not supplied, it will default to the config subfolder of the program file location.

External sources
Multiple resources used for reference information and ‘answers’ on how-to
•	StackExchange used on multiple occasions. Eg: c# - Find out when file is added to folder - Stack Overflow
•	Multiple Microsoft docs referenced eg: Create XML Trees in C# - LINQ to XML | Microsoft Docs
•	Some design pattern information from https://www.oodesign.com/

Assumptions
•	Assumes input files will have unique names. Output report files will be generated with names based on input filename & will overwrite previous file if present.

Incomplete
•	Internal structure of “GeneratorDataSet” object is exposed. This should be closed & made available through a visitor pattern or similar.
•	No unit tests or other formal test artifacts have been created.
•	Very limited logging has been implemented to support trouble-shooting etc.
