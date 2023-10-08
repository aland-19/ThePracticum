# ThePracticum

## Overview

The software exercise below is taken directly from the real world and is used by an organization to evaluate and gauge 
a developer's skill in taking a relatively simple, easy to understand, problem such as running a kitchen with breakfast, lunch and dinner items 
and maturing the simple implementation into a more sophisticated piece of software.  The opportunity is for the developer to 
showcase their abilities in using object oriented design and development techniques, along with unit and acceptance test and 
any other language features they want to explore and demonstrate their knowledge of.

In our specific case, we will take this opportunity to explore, discuss, learn and dig deeply into the following:
* Analysis of problem
* Design of a problem
* Object oriented modeling
* Various language features such as tuples, private functions, enumerations, etc...
* Dependency injection
* Single responsibility and other SOLID principles
* Unit and acceptance testing

We will leverage GitHub to learn and exercise Pull Request and continue to sharpen git skill such as branching, merging, tagging...

# .NET Developer Practicum - Refactoring

**Introduction:**

The .Net Developer Practicum is an exercise to extend the functionality of an existing solution.

**The .NET Developer practicum is evaluated on:**

1. Object Oriented Design
2. Readability
3. Maintainability
4. Testability
5. Extensibility

**Hints:**

- Take the time to walk through the existing functionality before trying to make changes
- Feel free to add 3rd party packages as needed

**Technical Requirements:**

Solution must:
- Compile without errors
- Run from the command line
- Pass all automated test cases
- Demonstrate your knowledge of automated testing by implementing both unit and acceptance tests

**Functional Requirements:**

1. Add ability to switch between morning and evening and have that be the first required parameter (case insensitive)
2. Add ability to have different dishes in the morning and at night (See sample input/output below)
3. You can have multiple orders of coffee in the morning (but still no more than 1 each of the other Dish Types)
4. Dessert is not available as a morning Dish Type
5. Preserve existing requirements:
    - You must enter a comma delimited list of Dish Types with at least one selection
    - The output must print Dish Names in the following order: entrée, side, drink, dessert
    - If invalid selection is encountered, then print &quot;error&quot;
    - Ignore whitespace in the input
    - Each Dish Type is optional (i.e. can have zero if not entered in the input)
    - You can have multiple orders of potatoes (but still no more than 1 each of the other Dish Types)
    - If more than one Dish Type is entered, output it once, followed by &quot;(xN)&quot;, e.g. &quot;potato(x2)&quot;

**Dishes for Morning**

| **Dish Type** | **Dish Name** |
| --- | --- |
| 1 (entrée) | egg |
| 2 (side) | toast |
| 3 (drink) | coffee |

**Dishes for Evening**

| **Dish Type** | **Dish Name** |
| --- | --- |
| 1 (entrée) | steak |
| 2 (side) | potato |
| 3 (drink) | wine |
| 4 (dessert) | cake |

**Sample Input and Output:**

| **Input** | **Output** |
| --- | --- |
| morning, 1, 2, 3 | egg,toast,coffee |
| Morning,3,3,3 | coffee(x3) |
| morning ,1,3,2,3 | egg,toast,coffee(x2) |
| morning, 1, 2, 2 | error |
| morning, 1, 2, 4 | error |
| evening,1, 2, 3, 4 | steak,potato,wine,cake |
| Evening,1, 2, 2, 4 | steak,potato(x2),cake |
| evening,1, 2, 3, 5 | error |
| evening,1, 3, 2, 3 | error |
