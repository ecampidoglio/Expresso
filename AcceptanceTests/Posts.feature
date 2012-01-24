Feature: Posts
	In order to decide wether I'm interested in reading the blog
	As a visitor
	I want to be able to see a complete list of posts

Scenario: Show the list of posts
	Given there are some posts on the blog
	When I navigate to the relative URL '/posts'
	Then I should see a list of posts with title and summary on the screen
