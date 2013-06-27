# Cineworld Portable #

## What is it? ##
Cineworld Portable is a Portable Class Library that gives developers access to the Cineworld film and cinema listings for the UK and Ireland ([www.cineworld.co.uk](http://cineworld.co.uk)).

## What targets does it have? ##
It targets ***everything***!!! So that's .NET 4.0.3+, Silverlight 5, Windows Store apps, Windows Phone 7.5, Windows Phone 8.

## Anything cool and unexpected? ##
Yeah, the main classes (Cinema, Film, Distributor, Performance and Category) all implement INotifyPropertyChanged to make it easier if you want to use them in an MVVM approach. There is also an `ICineworldClient` interface if you want to use it that way.

## How do I install it? ##
Nuget. Basically. 

PM> Install-Package CineworldPortable -Pre

## So what issues are there? ##
Well, it's built using the PCL version of HttpClient, which is now out of beta and has a go-live licence (yay!!), however, I've built this with the beta version that has support for automatic compression, which doesn't have a go-live licence, yet. So technically you're not meant to use it in any actual projects. So if you do, well, there's not a lot I can really do about that :)
