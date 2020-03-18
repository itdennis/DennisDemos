<font face="comic sans ms">

# The basics of MergeTextTool

This tool is implemented by .net core, you will get the basics usage of this tool in this guide.

## The code structure

- src

	- IDataSourceOps: Abstraction of data operator for other data source like database etc.

	- FileOps: Default implementation of IDataSourceOps, cover the basic operation method for file.

	- testdata folder: place the source file into this folder, after running the tool, the target file will be
	created at "./testdata/output/result.txt"

- tests

	- FileOpsTests: unit tests of `FileOps` class implemented by MSTest.

## Logic flow

![](https://s1.ax1x.com/2020/03/18/80Jg3V.png)


## Next step

- Add config file to provide parameters, now is hard code.
