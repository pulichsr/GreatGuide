package com.greatguide.ui.domain;

import java.io.Serializable;

/**
 * The {@code POI} represents a Point of Interest
 * 
 * @since Dec 28, 2012
 */
@SuppressWarnings( "serial" )
public class POI implements Serializable {

    private long id;
    private String name;
    private String description;

    /**
     * @param id unique identifies the POI
     * @param name the POI name
     * @param description a description
     */
    public POI( long id, String name, String description ) {
        super();
        this.id = id;
        this.name = name;
        this.description = description;
    }

    /**
     * @return the id
     */
    public long getId() {
        return id;
    }

    /**
     * @param id
     *            the id to set
     */
    public void setId( long id ) {
        this.id = id;
    }

    /**
     * @return the name
     */
    public String getName() {
        return name;
    }

    /**
     * @param name
     *            the name to set
     */
    public void setName( String name ) {
        this.name = name;
    }

    /**
     * @return the description
     */
    public String getDescription() {
        return description;
    }

    /**
     * @param description
     *            the description to set
     */
    public void setDescription( String description ) {
        this.description = description;
    }

}
