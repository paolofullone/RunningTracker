Feature: Running Tracker 

An api to track runs

@IntegratedTests

Scenario: Insert Run
	Given That we want to insert some runs:
	| Distance | Duration | Date                     |
	| 10       | 1:00:00  | 2024-10-14T07:00:00.000Z |
	| 12       | 1:12:00  | 2024-10-16T07:00:00.000Z |
	| 14       | 1:24:00  | 2024-10-18T07:00:00.000Z |
	| 15       | 1:30:00  | 2024-10-20T07:00:00.000Z |
	| 16       | 1:33:00  | 2024-10-22T07:00:00.000Z |
	| 17       | 1:36:00  | 2024-10-24T07:00:00.000Z |
	| 18       | 1:39:00  | 2024-10-26T07:00:00.000Z |
	When We request to insert some runs
	Then The runs are inserted, then the test validates and deletes them



