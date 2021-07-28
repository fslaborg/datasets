(**
---
title: Concussions in Male and Female College Athletes
category: Datasets
categoryindex: 1
index: 2
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# The _Concussions in Male and Female College Athletes_ dataset

**Table of contents**

- [Description]()
- [How to use]()
- [Examples]()

## Description

Counts of Concussions among collegiate athletes in 5 sports for 3 years by gender.  
Taken from [Lawrence H. Winner, University of Florida](http://archived.stat.ufl.edu/personnel/usrpages/winner.shtml):  
- [Data](http://users.stat.ufl.edu/~winner/data/concussion.dat)  
- [Description](http://users.stat.ufl.edu/~winner/data/concussion.txt)

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

let df = Frame.ReadCsvString(rawDataAdapted, hasHeaders = false, separators = "\t", schema = "Gender, Sports, Year, Concussion, Count")

// Otherwise, the following already adapted dataset can be used:
let rawData2 = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/ConcussionsInMaleAndFemaleCollegeAthletes_adapted.tsv"

let df2 = Frame.ReadCsvString(rawData2, hasHeaders = false, separators = "\t", schema = "Gender, Sports, Year, Concussion, Count")

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

// We need to filter out the columns and rows we don't need. Thus, we filter out the rows where the athletes suffered no concussions as well as filter out the columns without the number of concussions.
let dataAthletesFemale, dataAthletesMale =
    let getAthleteGenderData gender =
        let dataAthletesOnlyConcussion =
            df2
            |> Frame.filterRows (fun r objS -> objS.GetAs "Concussion")
        let dataAthletesGenderFrame =
            dataAthletesOnlyConcussion
            |> Frame.filterRows (fun r objS -> objS.GetAs "Gender" = gender)
        dataAthletesGenderFrame
        |> Frame.getCol "Count" 
        |> Series.values
        |> vector
    getAthleteGenderData "Female", getAthleteGenderData "Male"

let boxPlot = 
    [
        Chart.BoxPlot(y = dataAthletesFemale, Name = "female college athletes", Boxpoints = StyleParam.Boxpoints.All, Jitter = 0.2)
        Chart.BoxPlot(y = dataAthletesMale, Name = "male college athletes", Boxpoints = StyleParam.Boxpoints.All, Jitter = 0.2)
    ]
    |> Chart.Combine
    |> Chart.withY_AxisStyle "number of concussions over 3 years"

(*** condition: ipynb ***)
#if IPYNB
boxPlot
#endif // IPYNB

(***hide***)
boxPlot |> GenericChart.toChartHTML
(***include-it-raw***)

open FSharp.Stats.Testing

// We test both samples against each other, assuming equal variances.
let twoSampleResult = TTest.twoSample true dataAthletesFemale dataAthletesMale

(*** include-value:twoSampleResult ***)