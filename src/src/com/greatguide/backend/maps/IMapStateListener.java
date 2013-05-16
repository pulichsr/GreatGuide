package com.greatguide.backend.maps;

import java.util.ArrayList;

/**
 * Author: Lennie De Villiers
 */
public interface IMapStateListener {
    public void noMapsAvailable(ArrayList<String> aTiles);
    public void ZoomIn(int aNewZoomLevel);
    public void ZoomOut(int aNewZoomLevel);
    public void CantZoomIn(int aCurrentZoomLevel, int aAllowed);
    public void CantZoomOut(int aCurrentZoomLevel, int aAllowed);
}
