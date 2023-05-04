# Mutation Testing

## What?

- _small_ and _isolated_ modifications to production code
  - each modified version is called a `mutant`
- run (_impacted_) unit tests against each mutant
  - at least one _failing_ test will `kill` the mutant
  - no failing tests let the mutant `survive`
- effectiveness of test suites is measured by percentage of killed mutants

A mutant survives if it is not covered by an assertion.
**Kill all the mutants.**

---
###### [Why](./MutationTesting_CodeCoverage.md)
