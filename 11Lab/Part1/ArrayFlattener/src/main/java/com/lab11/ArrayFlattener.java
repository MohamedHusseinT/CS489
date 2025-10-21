package com.lab11;

/**
 * ArrayFlattener component that flattens a 2D nested array into a 1D array.
 * 
 * Example:
 * Input: [[1,3], [0], [4,5,9]]
 * Output: [1,3,0,4,5,9]
 */
public class ArrayFlattener {
    
    /**
     * Flattens a 2D nested array into a 1D array.
     * 
     * @param inputArray The 2D nested array to flatten
     * @return A flattened 1D array containing all elements from the nested arrays
     * @throws IllegalArgumentException if inputArray is null
     */
    public int[] flattenArray(int[][] inputArray) {
        if (inputArray == null) {
            throw new IllegalArgumentException("Input array cannot be null");
        }
        
        // Calculate total length of flattened array
        int totalLength = 0;
        for (int[] subArray : inputArray) {
            if (subArray != null) {
                totalLength += subArray.length;
            }
        }
        
        // Create result array
        int[] result = new int[totalLength];
        int index = 0;
        
        // Flatten the array
        for (int[] subArray : inputArray) {
            if (subArray != null) {
                for (int element : subArray) {
                    result[index++] = element;
                }
            }
        }
        
        return result;
    }
}

