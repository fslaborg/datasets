(**

# FsLab data sets
A data source for example datasets for all kinds of data science.

For now, this repository just aims to aggregate some data sources that can be referenced by HTTP requests in a data science project. In the future, this ideally evolves to a proper nuget package that provides the data as dataframes directly.
For using the data contained in this repository, we recommend using `FSharp.Data` in conjunction with `Deedle`, like this:

*)

#r "nuget: FSharp.Data"
#r "nuget: Deedle"

open FSharp.Data
open Deedle

let rawData = Http.RequestString @"https://raw.githubusercontent.com/fslaborg/datasets/main/data/iris.csv"
let df = Frame.ReadCsvString(rawData) //exact settings may differ here depending on e.g. the separator used in the individual dataset

df.Print()

(***include-output***)