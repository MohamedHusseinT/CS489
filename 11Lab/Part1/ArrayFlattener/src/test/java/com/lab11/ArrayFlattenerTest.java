package com.lab11;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeEach;
import static org.junit.jupiter.api.Assertions.*;

/**
 * JUnit test cases for ArrayFlattener component.
 * Tests the flattenArray() method with various input scenarios.
 */
public class ArrayFlattenerTest {
    
    private ArrayFlattener arrayFlattener;
    
    @BeforeEach
    void setUp() {
        arrayFlattener = new ArrayFlattener();
    }
    
    /**
     * Test case: When the input is a legitimate 2D nested array.
     * Input: [[1,3], [0], [4,5,9]]
     * Expected Output: [1,3,0,4,5,9]
     */
    @Test
    void testFlattenArray_WithValid2DArray() {
        // Arrange
        int[][] inputArray = {{1, 3}, {0}, {4, 5, 9}};
        int[] expectedOutput = {1, 3, 0, 4, 5, 9};
        
        // Act
        int[] actualOutput = arrayFlattener.flattenArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput, 
            "Flattened array should match expected output");
        assertEquals(6, actualOutput.length, 
            "Flattened array should have correct length");
    }
    
    /**
     * Test case: When the input is null.
     * Expected: IllegalArgumentException should be thrown
     */
    @Test
    void testFlattenArray_WithNullInput() {
        // Arrange
        int[][] inputArray = null;
        
        // Act & Assert
        IllegalArgumentException exception = assertThrows(
            IllegalArgumentException.class,
            () -> arrayFlattener.flattenArray(inputArray),
            "Should throw IllegalArgumentException when input is null"
        );
        
        assertEquals("Input array cannot be null", exception.getMessage(),
            "Exception message should be correct");
    }
    
    /**
     * Test case: When the input is an empty 2D array.
     * Expected Output: Empty array
     */
    @Test
    void testFlattenArray_WithEmptyArray() {
        // Arrange
        int[][] inputArray = {};
        int[] expectedOutput = {};
        
        // Act
        int[] actualOutput = arrayFlattener.flattenArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput,
            "Empty input should result in empty output");
        assertEquals(0, actualOutput.length,
            "Empty input should result in zero-length output");
    }
    
    /**
     * Test case: When the input contains null sub-arrays.
     * Input: [[1, 2], null, [3, 4]]
     * Expected Output: [1, 2, 3, 4]
     */
    @Test
    void testFlattenArray_WithNullSubArrays() {
        // Arrange
        int[][] inputArray = {{1, 2}, null, {3, 4}};
        int[] expectedOutput = {1, 2, 3, 4};
        
        // Act
        int[] actualOutput = arrayFlattener.flattenArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput,
            "Should handle null sub-arrays correctly");
    }
}

