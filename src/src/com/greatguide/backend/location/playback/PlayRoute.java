package com.greatguide.backend.location.playback;

import java.util.List;

/**
 * Author: Lennie De Villiers
 */
public class PlayRoute {
    private List<PlayRouteLocation> _routePointList;
    private int _delay;

    public PlayRoute(List<PlayRouteLocation> aRoutePointList, int aDelay)
    {
        _delay =  aDelay;
        _routePointList = aRoutePointList;
    }

    public List<PlayRouteLocation> getRoutePointList()
    {
        return _routePointList;
    }

    /**
     *
     * Get the actual delay period in seconds
     *
     * @return
     */
    public int getActualDelay()
    {
        return _delay;
    }

    /**
     *
     * Get the delay period in milliseconds (seconds * 1000)
     *
     * @return
     */
    public int getDelay()
    {
        return _delay * 1000;
    }
}
