package com.lab11;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

/**
 * JUnit test cases for ArrayReversor component using Mockito for mocking.
 * Tests the reverseArray() method with various input scenarios and verifies
 * that the ArrayFlattenerService is properly invoked.
 */
@ExtendWith(MockitoExtension.class)
public class ArrayReversorTest {
    
    @Mock
    private ArrayFlattenerService mockFlattenerService;
    
    private ArrayReversor arrayReversor;
    
    @BeforeEach
    void setUp() {
        arrayReversor = new ArrayReversor(mockFlattenerService);
    }
    
    /**
     * Test case: When the input is a legitimate 2D nested array.
     * Input: [[1,3], [0], [4,5,9]]
     * Expected: Service is called, and result is reversed [9,5,4,0,3,1]
     */
    @Test
    void testReverseArray_WithValid2DArray() {
        // Arrange
        int[][] inputArray = {{1, 3}, {0}, {4, 5, 9}};
        int[] flattenedArray = {1, 3, 0, 4, 5, 9};
        int[] expectedOutput = {9, 5, 4, 0, 3, 1};
        
        // Configure mock behavior
        when(mockFlattenerService.flattenArray(inputArray)).thenReturn(flattenedArray);
        
        // Act
        int[] actualOutput = arrayReversor.reverseArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput,
            "Reversed array should match expected output");
        assertEquals(6, actualOutput.length,
            "Reversed array should have correct length");
        
        // Verify that the service was called exactly once with the correct input
        verify(mockFlattenerService, times(1)).flattenArray(inputArray);
        verifyNoMoreInteractions(mockFlattenerService);
    }
    
    /**
     * Test case: When the input is null.
     * Expected: IllegalArgumentException should be thrown, service should not be called
     */
    @Test
    void testReverseArray_WithNullInput() {
        // Arrange
        int[][] inputArray = null;
        
        // Act & Assert
        IllegalArgumentException exception = assertThrows(
            IllegalArgumentException.class,
            () -> arrayReversor.reverseArray(inputArray),
            "Should throw IllegalArgumentException when input is null"
        );
        
        assertEquals("Input array cannot be null", exception.getMessage(),
            "Exception message should be correct");
        
        // Verify that the service was never called
        verifyNoInteractions(mockFlattenerService);
    }
    
    /**
     * Test case: When the input is an empty 2D array.
     * Expected: Service is called, and result is empty array
     */
    @Test
    void testReverseArray_WithEmptyArray() {
        // Arrange
        int[][] inputArray = {};
        int[] flattenedArray = {};
        int[] expectedOutput = {};
        
        // Configure mock behavior
        when(mockFlattenerService.flattenArray(inputArray)).thenReturn(flattenedArray);
        
        // Act
        int[] actualOutput = arrayReversor.reverseArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput,
            "Empty input should result in empty output");
        assertEquals(0, actualOutput.length,
            "Empty input should result in zero-length output");
        
        // Verify that the service was called exactly once
        verify(mockFlattenerService, times(1)).flattenArray(inputArray);
    }
    
    /**
     * Test case: When the flattened array has single element.
     * Input: [[5]]
     * Expected: Service is called, and result is [5] (single element reversed is itself)
     */
    @Test
    void testReverseArray_WithSingleElement() {
        // Arrange
        int[][] inputArray = {{5}};
        int[] flattenedArray = {5};
        int[] expectedOutput = {5};
        
        // Configure mock behavior
        when(mockFlattenerService.flattenArray(inputArray)).thenReturn(flattenedArray);
        
        // Act
        int[] actualOutput = arrayReversor.reverseArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput,
            "Single element array should remain unchanged when reversed");
        
        // Verify that the service was called exactly once
        verify(mockFlattenerService, times(1)).flattenArray(inputArray);
    }
    
    /**
     * Test case: Verify that the service is actually used and not bypassed.
     * This test ensures that the ArrayReversor doesn't hard-code results
     * and actually depends on the ArrayFlattenerService.
     */
    @Test
    void testReverseArray_ServiceDependencyVerification() {
        // Arrange
        int[][] inputArray = {{1, 2}, {3, 4}};
        int[] flattenedArray = {1, 2, 3, 4};
        int[] expectedOutput = {4, 3, 2, 1};
        
        // Configure mock to return different result to verify dependency
        when(mockFlattenerService.flattenArray(inputArray)).thenReturn(flattenedArray);
        
        // Act
        int[] actualOutput = arrayReversor.reverseArray(inputArray);
        
        // Assert
        assertArrayEquals(expectedOutput, actualOutput,
            "Result should depend on service output, not be hard-coded");
        
        // Verify service was called and result depends on its output
        verify(mockFlattenerService, times(1)).flattenArray(inputArray);
        
        // Test with different service output to ensure dependency
        int[] differentFlattenedArray = {5, 6, 7, 8};
        int[] differentExpectedOutput = {8, 7, 6, 5};
        
        when(mockFlattenerService.flattenArray(inputArray)).thenReturn(differentFlattenedArray);
        
        int[] secondOutput = arrayReversor.reverseArray(inputArray);
        assertArrayEquals(differentExpectedOutput, secondOutput,
            "Result should change when service output changes");
        
        // Verify service was called twice total
        verify(mockFlattenerService, times(2)).flattenArray(inputArray);
    }
}

