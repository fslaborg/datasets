(**
---
title: Iris
category: Datasets
categoryindex: 1
index: 1
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# The Iris dataset

**Table of contents**

- [Description]()
- [How to use]()
- [Examples]()

## Description

WIP

## How to use

*)

#r "nuget: FSharp.Data"
#r "nuget: Deedle"

open FSharp.Data
open Deedle

let rawData = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/iris.csv"
let df = Frame.ReadCsvString(rawData) //exact settings may differ here depending on e.g. the separator used in the individual dataset

df.Print()

(*** include-output ***)

(**

## Examples

WIP

*)