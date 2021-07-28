(**
---
title: Housefly wing length
category: Datasets
categoryindex: 1
index: 4
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# The _housefly wing length_ dataset

**Table of contents**

- [Description]()
- [How to use]()
- [Examples]()

## Description

Measured wing lengths of 100 houseflies in mm * 10^1.
Taken from https://seattlecentral.edu/qelp/sets/057/057.html

Original literature: Sokal, R.R. and P.E. Hunter. 1955. _A morphometric analysis of DDT-resistant and non-resistant housefly strains_ Ann. Entomol. Soc. Amer. 48: 499-507.


## How to use

*)

#r "nuget: FSharp.Data"
#r "nuget: Deedle"

open FSharp.Data
open Deedle

let rawData = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/HouseflyWingLength.txt"

let dataFrame = Frame.ReadCsvString(rawData, hasHeaders = false, schema = "wing length (mm * 10^1)")

dataFrame.Print()

(*** include-output ***)

(**

## Examples

This example is taken from the FsLab datascience tutorial [t-test]()
(WIP)

*)

#r "nuget: FSharp.Stats, 0.4.2"
#r "nuget: Plotly.NET, 2.0.0-preview6"

open FSharp.Stats
open FSharp.Stats.Testing
open Plotly.NET

let boxPlot = 
    Chart.BoxPlot(y = dataHousefly, Name = "housefly", Boxpoints = StyleParam.Boxpoints.All, Jitter = 0.2)
    |> Chart.withY_AxisStyle "wing length [mm]"

(*** condition: ipynb ***)
#if IPYNB
boxPlot
#endif // IPYNB

(***hide***)
boxPlot |> GenericChart.toChartHTML
(***include-it-raw***)

// The testing module in FSharp.Stats require vectors as input types, thus we transform our array into a vector:
let vectorDataHousefly = vector dataHousefly

// The expected value of our population.
let expectedValue = 45.

// Perform the one-sample t-test with our vectorized data and our exptected value as parameters.
let oneSampleResult = TTest.oneSample vectorDataHousefly expectedValue

(*** hide ***)

(*** include-value:oneSampleResult ***)