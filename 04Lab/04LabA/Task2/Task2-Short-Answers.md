# Lab 4A - Task 2: Short Answers

## What is Spring?

**Answer:** Spring is a lightweight, comprehensive framework for enterprise Java development that simplifies the complexity of enterprise application development.

**Example:** Instead of manually creating objects and managing dependencies:
```java
// Without Spring - Manual object creation
BookService bookService = new BookService();
AuthorService authorService = new AuthorService(bookService);

// With Spring - Dependency injection context
@Autowired
private BookService bookService;

@Autowired 
private AuthorService authorService;
```

## What is Spring Boot?

**Answer:** Spring Boot is an extension of the Spring framework that provides auto-configuration, starter dependencies, and embedded servers to create production-ready applications with minimal configuration.

**Example:** 
```java
// Traditional Spring requires XML configuration
@Configuration
public class Config {
    @Bean
    public DataSource dataSource() {
        // Complex configuration
    }
}

// Spring Boot auto-configures everything
// Just add starter dependencies in pom.xml
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-web</artifactId>
</dependency>
```

## What is the relation between Spring platform and Spring Boot?

**Answer:** Spring Boot is built on top of the Spring platform/ecosystem and leverages its core components while adding conventions that simplify development and deployment.

**Relation:**
- **Spring Platform**: Foundation that includes Spring Core, Spring MVC, Spring Security, etc.
- **Spring Boot**: Opinionated framework that uses Spring Platform components with auto-configuration

**Example:**
```
Spring Platform (Foundation)
    ├── Spring Core (IoC Container)
    ├── Spring MVC (Web Layer)
    ├── Spring Data (Repository Layer)
    └── Spring Security (Security Layer)
            ↓
Spring Boot (Application Framework)
    ├── Auto-configuration (Uses Spring components automatically)
    ├── Starter Dependencies (Includes multiple Spring modules)
    └── Embedded Server (Tomcat, Jetty, Undertow)
```

## What is the relation between Spring platform and Spring framework?

**Answer:** The Spring platform IS the Spring framework - they are the same thing. "Spring platform" is sometimes used to describe the entire ecosystem of Spring projects and modules.

**Example:**
- Spring Framework Core = Core platform components
- Spring Platform = All Spring projects together
```java
// Spring Framework Core
@Component
public class BookService {
    // Core Spring annotation
}

// Spring Platform includes:
// - Spring Framework
// - Spring Data  
// - Spring Security
// - Spring Cloud
// - Spring Boot
```

## What is Dependency Injection and how is it done in the Spring platform/framework?

**Answer:** Dependency Injection (DI) is a design pattern where dependencies are provided to a class rather than the class creating them itself.

**How in Spring:**
1. **Constructor Injection** (Recommended)
2. **Setter Injection**  
3. **Field Injection**

**Examples:**

**Constructor Injection:**
```java
@Service
public class BookService {
    private final AuthorService authorService;
    
    // Spring automatically injects AuthorService
    public BookService(AuthorService authorService) {
        this.authorService = authorService;
    }
}
```

**Setter Injection:**
```java
@Service  
public class BookService {
    @Autowired
    public void setAuthorService(AuthorService authorService) {
        this.authorService = authorService;
    }
}
```

**Field Injection:**
```java
@Service
public class BookService {
    @Autowired
    private AuthorService authorService;
}
```

## What is Inversion of Control (IoC) and how is it related to Spring?

**Answer:** Inversion of Control (IoC) is a principle where the control of object creation and lifecycle is inverted from the application code to a container/framework.

**How it relates to Spring:**
- Spring acts as an IoC container
- Spring manages the creation, configuration, and lifecycle of beans
- Applications define what they need; Spring provides it

**Example:**

**Without IoC (Traditional):**
```java
public class BookController {
    private BookService bookService;
    
    public BookController() {
        // Application controls object creation
        this.bookService = new BookService();
    }
}
```

**With Spring IoC:**
```java
@RestController
public class BookController {
    private final BookService bookService;
    
    // Spring controls object creation and injection
    public BookController(BookService bookService) {
        this.bookService = bookService; // Injected by Spring
    }
}
```

**Spring IoC Container Process:**
1. **Scan**: Spring scans for `@Component`, `@Service`, `@Repository`, `@Controller`
2. **Create**: Spring creates bean instances
3. **Wire**: Spring injects dependencies using DI
4. **Manage**: Spring manages the entire lifecycle
