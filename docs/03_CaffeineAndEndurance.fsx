(**
---
title: Caffeine and endurance
category: Datasets
categoryindex: 1
index: 3
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# The _caffeine and endurance_ dataset

**Table of contents**

- [Description]()
- [How to use]()
- [Examples]()

## Description

Endurance times for 9 well-trained cyclists, on each of 4 doses of caffeine (0, 5, 9, 13 mg) with 1 line per subject.
Taken from [Lawrence H. Winner, University of Florida](http://archived.stat.ufl.edu/personnel/usrpages/winner.shtml):  
- [Data](http://users.stat.ufl.edu/~winner/data/caffeine1.dat)
- [Description](http://users.stat.ufl.edu/~winner/data/caffeine1.txt)

Original literature: W.J. Pasman, M.A. van Baak, A.E. Jeukendrup, A. de Haan (1995). "The Effect of Different Dosages of Caffeine on Endurance Performance Time", International Journal of Sports Medicine, Vol. 16, pp225-230.


## How to use

*)

#r "nuget: FSharp.Data"
#r "nuget: Deedle"

open FSharp.Data
open Deedle
open System.Text.RegularExpressions

let rawDataCaffeine = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/CaffeineAndEndurance(wide).dat"
// This data format features a char column-wise structure. To transform it into a seperator-delimited format, we have to replace the multiple spaces via Regex:
let regexCaffeine = [Regex("[ ]{2,}1"), "1"; Regex("[ ]{2,}"), "\t"; Regex("\n\t"), "\n"]
let rawDataCaffeineAdapted = 
    regexCaffeine
    |> List.fold (fun acc (reg,rep) -> reg.Replace(acc, rep)) rawDataCaffeine

let df = Frame.ReadCsvString(rawDataCaffeineAdapted, hasHeaders = false, separators = "\t", schema = "Subject ID, no Dose, 5 mg, 9 mg, 13 mg")

// Otherwise, the following already adapted dataset can be used:
let rawData2 = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/CaffeineAndEndurance(wide)_adapted.tsv"

let df2 = Frame.ReadCsvString(rawData2, hasHeaders = false, separators = "\t", schema = "Subject ID, no Dose, 5 mg, 9 mg, 13 mg")

df2.Print()

(*** include-output ***)

(**

## Examples

This example is taken from the FsLab datascience tutorial [t-test]()
(WIP)

*)

#r "nuget: FSharp.Stats, 0.4.2"
#r "nuget: Plotly.NET, 2.0.0-preview.6"

open FSharp.Stats
open FSharp.Stats.Testing
open Plotly.NET

// We want to compare the subjects' performances under the influence of 13 mg caffeine and in the control situation.
let dataCaffeineNoDose, dataCaffeine13mg =
    let getVectorFromCol col = 
        df2
        |> Frame.getCol col
        |> Series.values
        |> vector
    getVectorFromCol "no Dose", getVectorFromCol "13 mg"

// 
let visualizePairedData = 
    Seq.zip dataCaffeineNoDose dataCaffeine13mg
    |> Seq.mapi (fun i (control,treatment) -> 
        let participant = "Person " + string (i + 1)
        Chart.Line(["no dose", control; "13 mg", treatment], Name = participant)
        )
    |> Chart.Combine
    |> Chart.withX_AxisStyle ""
    |> Chart.withY_AxisStyle("endurance performance", MinMax = (0.,100.))


(*** condition: ipynb ***)
#if IPYNB
visualizePairedData
#endif // IPYNB

(***hide***)
visualizePairedData |> GenericChart.toChartHTML
(***include-it-raw***)

let twoSamplePairedResult = TTest.twoSamplePaired dataCaffeineNoDose dataCaffeine13mg

(*** include-value:twoSamplePairedResult ***)