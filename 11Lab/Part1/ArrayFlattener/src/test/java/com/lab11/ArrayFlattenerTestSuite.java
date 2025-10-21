package com.lab11;

import org.junit.platform.suite.api.SelectClasses;
import org.junit.platform.suite.api.Suite;

/**
 * JUnit TestSuite containing all test cases for ArrayFlattener component.
 * This suite runs all the test cases defined in ArrayFlattenerTest.
 */
@Suite
@SelectClasses(ArrayFlattenerTest.class)
public class ArrayFlattenerTestSuite {
    // This class serves as a container for the test suite.
    // JUnit will automatically discover and run all tests in ArrayFlattenerTest.
}

