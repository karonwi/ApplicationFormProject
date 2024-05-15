# Application Form Management

This project is designed to facilitate the creation and management of application forms

### Key Enhancements
- **CRUD Operations for Questions**: Implemented CRUD endpoints to manage different types of questions including Paragraph, YesNo, Dropdown, MultipleChoice, Date, and Number.
- **Form Submission**: Developed endpoints to allow candidates to submit their answers based on the question types.
- **Data Management**: Integrated Azure Cosmos DB for efficient data handling and optimized query performance using appropriate partition keys.


## Question Types

The application uses enums to categorize question types. Here are the different question types supported:

- `0`: Paragraph
- `1`: YesNo
- `2`: Dropdown
- `3`: MultipleChoice
- `4`: Date
- `5`: Number

Each question type corresponds to a specific structure in the application forms, allowing for dynamic form handling based on question type.
## API Endpoints

### Adding Questions

To add questions to the system, use the POST method at the `/api/questions` endpoint. Here is how to format the request for each type of question:

#### Add a Paragraph Question

```bash
curl -X POST http://localhost:5000/api/questions -H 'Content-Type: application/json' -d '{
    "type": 0,
    "content": "Please describe your previous job experience.",
    "isRequired": true
}'
```

#### Add a Yes/No Question
```bash
curl -X POST http://localhost:5000/api/questions -H 'Content-Type: application/json' -d '{
    "type": 1,
    "content": "Do you have previous experience in this field?",
    "isRequired": true
}'
```
#### Retrieving Questions
#### To fetch questions, use the GET endpoint:
```bash
curl -X GET http://localhost:5000/api/questions
```

#### Retrieving Questions
#### Submitting Application Forms
```bash
curl -X POST http://localhost:5000/api/applications -H 'Content-Type: application/json' -d '{
    "userId": "user-id-here",
    "answers": [
        {"questionId": "question-id-1", "answers": ["Your detailed response here"]},
        {"questionId": "question-id-2", "answers": ["Yes"]}
    ]
}'
```
