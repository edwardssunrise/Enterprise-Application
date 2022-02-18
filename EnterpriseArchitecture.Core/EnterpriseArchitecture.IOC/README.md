## IOC (Inversion Of Control)

IoC (Inversion Of Control) is a software development principle that aims to create objects that are loose coupling throughout the lifecycle of the application. It is responsible for the life cycle of objects, provides their management. When an interface is injected into the class using IoC, the corresponding interface methods become available. Thus, the class using IoC only knows the methods it will use, even if there are more methods in the class, it will be able to access the methods specified in the interface.

It enables us to develop rewritable and testable software since the class using IoC will not be affected by any changes to be made in the class. IoC object bindings are usually configured at application startup. In this sense, it is also very easy to change and manage IoC configurations made from a single place.

We can list the advantages of using IoC as follows:

1. Creating loosely coupled classes
2. Easy unit test writing
3. Manageability
4. Modular programs
5. Easy transition between different implementations