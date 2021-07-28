(**
---
title: Concussions in male and female college athletes
category: Datasets
categoryindex: 1
index: 2
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# The _Concussions in male and female college athletes_ dataset

**Table of contents**

- [Description]()
- [How to use]()
- [Examples]()

## Description

Counts of Concussions among collegiate athletes in 5 sports for 3 years by gender.
Taken from [Lawrence H. Winner, University of Florida](http://archived.stat.ufl.edu/personnel/usrpages/winner.shtml):
- [Data](http://users.stat.ufl.edu/~winner/data/concussion.dat)
- [Description](http://users.stat.ufl.edu/~winner/data/psycparole.txt)

Original literature: T. Covassin, C.B. Swanik, M.L. Sachs (2003). "Sex Differences and the Incidence of Concussions Among Collegiate Athletes", Journal of Athletic Training, Vol. (38)3, pp238-244


## How to use

*)

#r "nuget: FSharp.Data"
#r "nuget: Deedle"

open FSharp.Data
open Deedle
open System.Text.RegularExpressions

let rawData = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/ConcussionsInMaleAndFemaleCollegeAthletes.dat"

// This data format features a char column-wise structure. To transform it into a seperator-delimited format, we have to replace the multiple spaces via Regex:
let regex = Regex("[ ]{2,}")
let rawDataAdapted = regex.Replace(rawData, "\t")

let dataFrame = Frame.ReadCsvString(rawDataAdapted, hasHeaders = false, separators = "\t", schema = "Gender, Sports, Year, Concussion, Count")

// Otherwise, the following already adapted dataset can be used:
let rawData2 = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/ConcussionsInMaleAndFemaleCollegeAthletes_Adapted.tsv"

let dataFrame2 = Frame.ReadCsvString(rawData2, hasHeaders = false, separators = "\t", schema = "Gender, Sports, Year, Concussion, Count")

dataFrame2.Print()

(*** include-output ***)

(**

## Examples

This example is taken from the FsLab datascience tutorial [t-test]()
(WIP)

*)