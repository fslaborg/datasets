(**
---
title: pvalues
category: Datasets
categoryindex: 1
index: 7
---

[![Binder]({{root}}img/badge-binder.svg)](https://mybinder.org/v2/gh/plotly/Plotly.NET/gh-pages?filepath={{fsdocs-source-basename}}.ipynb)&emsp;
[![Script]({{root}}img/badge-script.svg)]({{root}}{{fsdocs-source-basename}}.fsx)&emsp;
[![Notebook]({{root}}img/badge-notebook.svg)]({{root}}{{fsdocs-source-basename}}.ipynb)

# Description

Thousands of features were measured in triplicates at a control and treatment condition. 9856 comparisons were conducted with t tests and the p values were isolated. 
The p valued distribution does not follow an uniform distribution because there are true effects for a majority of the tested features .

# How to use

*)

#r "nuget: FSharp.Data"


open FSharp.Data


let rawData = Http.RequestString @"https://raw.githubusercontent.com/bvenn/datasets/main/data/pvalExample.txt"

let pvalues = 
    rawData.Split '\n'
    |> Array.tail
    |> Array.filter (fun x -> x <> "")
    |> Array.map float

pvalues
(*** include-output ***)

(**


*)