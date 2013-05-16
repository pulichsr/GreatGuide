/**
 *
 */
package com.greatguide.ui.test.media;

import java.io.File;
import java.io.FilenameFilter;

/**
 * @author sandeep pulichintala
 */
class MediaPlayerFilter implements FilenameFilter {

    /**
     * To accept the formats .mp3,.mp4,.wav
     */

    public boolean accept(File dir, String name) {

        if (name.endsWith(".mp3") || name.endsWith(".mp4")
                || name.endsWith(".wav"))
            return true;
        else
            return false;
    }

}
