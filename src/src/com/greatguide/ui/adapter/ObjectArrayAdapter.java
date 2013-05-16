package com.greatguide.ui.adapter;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;

/**
 * The {@code ObjectArrayAdapter} list adapter to display data in a list
 * @since Dec 28, 2012
 */
public class ObjectArrayAdapter<DATA, LAYOUT extends View> extends ArrayAdapter<DATA> {
    
    public static interface ObjectMapper<DATA, LAYOUT> {
        void map(LAYOUT view, DATA object);
    }

    private final ObjectMapper<DATA, LAYOUT> mapper;
    private final int layout;
    private List<DATA> objects;

    /**
     * Constructor
     * @param context application context
     * @param layout the list item layout 
     * @param objects the domain objects to list
     * @param mapper maps data to view
     */
    public ObjectArrayAdapter(Context context, int layout, List<DATA> objects, ObjectMapper<DATA, LAYOUT> mapper) {
        super(context, layout, objects);
        this.mapper = mapper;
        this.layout = layout;
        this.objects = objects;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    @SuppressWarnings( "unchecked" )
    public View getView( int position, View convertView, ViewGroup parent ) {
        LAYOUT view = (LAYOUT) convertView;
        if (view ==  null) {
            view = (LAYOUT) LayoutInflater.from(getContext()).inflate(layout, null);
        }
        
        DATA object = objects.get(position);
        mapper.map(view, object);
        
        return view;
    }
    
    
}
