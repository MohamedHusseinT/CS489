package com.lab11;

import org.junit.platform.suite.api.SelectClasses;
import org.junit.platform.suite.api.Suite;

/**
 * JUnit TestSuite named ArrayReversorTestCases containing all test cases 
 * for ArrayReversor component.
 * This suite runs all the test cases defined in ArrayReversorTest.
 */
@Suite
@SelectClasses(ArrayReversorTest.class)
public class ArrayReversorTestCases {
    // This class serves as a container for the test suite.
    // JUnit will automatically discover and run all tests in ArrayReversorTest.
}

