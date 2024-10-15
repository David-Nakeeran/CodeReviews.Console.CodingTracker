## Coding Tracker

Console based CRUD application to track number of coding hours.
Developed using C# , SqLite and Spectre.Console.

## Requirements

1. **Coding Tracking**:

   - Record daily coding hours.

2. **User Input**:

   - Users need to be able to input the start and end time of coding session.

3. **Database Management**:

   - Application should store and retrieve data from a real database.
   - Upon application start, it should create a sqlite database, if one isn’t present.

4. **CRUD**:

   - Users should be able to insert, delete, update and view their coding sessions.

5. **Error Handling**:

   - Application should handle all possible errors so that the application never crashes.

6. **Database**:

   - Dapper ORM for data access.
   - Table from database must be read into a List of Coding Sessions.

7. **Follow DRY Principle**:

   - Avoid code repetition.

8. **Separation of Concerns**:

   - Object-Oriented Programming

9. **Configuration File**:

   - Configuration file will contain database path and connection strings.

## Features

- Uses Spectre.Console to enhance user interaction.
- View all records: List all stored entries, including IDs, start and end times as well as duration and date of entry.
- Add a new record: Insert a new coding session.
- Update existing record: Update a specific record by its ID.
- Delete a record: Remove a coding session by its ID.
