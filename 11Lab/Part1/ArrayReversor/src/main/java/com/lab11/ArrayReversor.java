package com.lab11;

/**
 * ArrayReversor component that reverses a 2D nested array by first flattening it
 * and then reversing the flattened array.
 * 
 * Example:
 * Input: [[1,3], [0], [4,5,9]]
 * After flattening: [1,3,0,4,5,9]
 * After reversing: [9,5,4,0,3,1]
 */
public class ArrayReversor {
    
    private final ArrayFlattenerService flattenerService;
    
    /**
     * Constructor that accepts an ArrayFlattenerService dependency.
     * 
     * @param flattenerService The service used to flatten arrays
     */
    public ArrayReversor(ArrayFlattenerService flattenerService) {
        this.flattenerService = flattenerService;
    }
    
    /**
     * Reverses a 2D nested array by first flattening it and then reversing the result.
     * 
     * @param inputArray The 2D nested array to reverse
     * @return A reversed 1D array containing all elements from the nested arrays
     * @throws IllegalArgumentException if inputArray is null
     */
    public int[] reverseArray(int[][] inputArray) {
        if (inputArray == null) {
            throw new IllegalArgumentException("Input array cannot be null");
        }
        
        // Use the remote service to flatten the array
        int[] flattenedArray = flattenerService.flattenArray(inputArray);
        
        // Reverse the flattened array
        int[] reversedArray = new int[flattenedArray.length];
        for (int i = 0; i < flattenedArray.length; i++) {
            reversedArray[i] = flattenedArray[flattenedArray.length - 1 - i];
        }
        
        return reversedArray;
    }
}

