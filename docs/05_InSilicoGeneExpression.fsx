(**
---
title: In silico gene expression
category: Datasets
categoryindex: 1
index: 5
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# The _in silico gene expression_ dataset

**Table of contents**

- [Description]()
- [How to use]()
- [Examples]()

## Description

This is an in-silico data. It emulates the expression of 100 genes over 3 conditions, with 3 replicates each. It is made so that replicates are more similar to each other.

7% of the values are dropped, as the dataset was originally made to showcase missing value imputation.

## How to use

*)

#r "nuget: FSharp.Data"
#r "nuget: Deedle"

open FSharp.Data
open Deedle

let rawData = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/InSilicoGeneExpression.csv"

let df : Frame<string,string> = 
    Frame.ReadCsvString(rawData)
    |> Frame.indexRows "Key" //exact settings may differ here depending on e.g. the separator used in the individual dataset

df.Print()

(*** include-output ***)

(**

## Examples

Compute a correlation matrix between the genes after imputing the missing values

*)

#r "nuget: FSharp.Stats, 0.4.2"
#r "nuget: Plotly.NET, 2.0.0-preview.6"

open FSharp.Stats
open FSharp.Stats.ML
open Plotly.NET

// Select the imputation method: kNearestImpute where the 2 nearest observations are considered
let kn : Impute.MatrixBaseImputation<float[],float> = Impute.kNearestImpute 2

// Impute the missing values using the "imputeBy" function. The values of the deedle frame are first transformed into the input type of this function.
let imputedData = 
    df 
    |> Frame.toJaggedArray 
    |> Impute.imputeBy kn Ops.isNan
    |> Matrix.ofJaggedSeq

// Perform a row-wise pearson correlation on the matrix, resulting in a correlation matrix
let correlationMatrix = Correlation.Matrix.rowWisePearson imputedData

// Create a plotly heatmap from the correlation matrix
let correlationHeatmap = 
    correlationMatrix
    |> Matrix.toJaggedArray
    |> Chart.Heatmap

(*** condition: ipynb ***)
#if IPYNB
correlationHeatmap
#endif // IPYNB

(***hide***)
correlationHeatmap |> GenericChart.toChartHTML
(***include-it-raw***)
