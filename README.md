# Overview #

Application is designed to generate fake user's data with support for selecting a region for data generation.
Application must support input error emulation and randomization based on user input.

## Requirements #

*	Application have single index page.
*	Support for at least 3 generation regions (country + language).
*	Data for generation: first and last name, home address and phone number. 
*	Support for 3 error types:
	* Removing single character in the random position.
	* Adding single random character in a random position (from the alphabet corresponding to the region).
	* Swapping two adjacent random characters. 
*	Setting count of errors for single entry using slider with range from 0 to 10 with associated umber editor with range from 0 to 1000. 
*	Editor with seed data for randomization and "Random" button. 
*	After settings change, data must be regenerated. 
*	Data table supports infinite scrolling: by default loaded 20 items, when scrolling, next 10 items are loaded. 
*	Data at the table: item number, random ID, first and last name, home address (different formats), phone number (preferably in different formats).
*	User data is generated based on the number entered by the user: with the same settings the same data should be generated. 
*	Application cannot store the generated data.

### Details #

#### What is a „input error”? #

It is emulation of uncorrect user input. 
Measured in the number of the errors for the single generated item. 
Number can be in range between 0 and 10 with an accuracy of 0.25. 
When number is equal 0.5, then an error will be generated with probability 50%. 
If number is 2.25, then generated item will have 2 errors and 3rd error will be generated with probability 25%.
