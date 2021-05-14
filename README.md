# SportsPlayback

A Unity project that uses tracking data from real soccer players and visualizes it in 3D.
The project asynchronously reads tracking data from a local file, processes it, populates the internal data model, interpolates the data to match 60 FPS and visualizes it.

[Video](https://drive.google.com/file/d/18TE2D57dRuP_P4cO03c4ySldoR9wPbDR/view?usp=sharing)

The solution is simple, but can be extended to support the other ways of receiving data and other data formats.
```
T1 is input data and T2 is output data (internal)

* PlaybackPlayer<T1, T2> - main MonoBehaviour that aggregates PlaybackEngine and PlaybackVisualizer
  *  PlaybackEngine<T1, T2> - responsible for streaming data and populating data model
    * IPlaybackDataProvider<T1> - async data provider
    * IPlaybackDataProcessor<T1, T2> - async data processor 
  *  PlaybackVisualizer<T2> - responsible for looping through the data model and interpolating the data
* MatchView - view implementation that spawns and manipulates view game objects based on the interpolated data
```
