package com.greatguide.ui;

import com.greatguide.backend.media.IMediaPlayerListener;

/**
 * Author: Lennie De Villiers
 * Created: 01 Dec 2012
 */
public interface IServiceEventListener extends IMediaPlayerListener {
    public void handleAll(String aAction, String aEvent);
}
