package com.lab11;

/**
 * ArrayFlattenerService interface for remote service that flattens 2D arrays.
 * This service is remote/unavailable and will be mocked in tests.
 */
public interface ArrayFlattenerService {
    
    /**
     * Flattens a 2D nested array into a 1D array.
     * 
     * @param inputArray The 2D nested array to flatten
     * @return A flattened 1D array containing all elements from the nested arrays
     */
    int[] flattenArray(int[][] inputArray);
}

