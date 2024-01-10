# **Dialogue System Documentation**
## **Overview**
The code provides a simple structure for creating and managing dialogue nodes within a conversation. It allows the creation of dialogue nodes with associated NPC speech and player response options.

## **Classes**
### **DialogueNode**
- Represents a node in the dialogue tree.
##### **Properties (DialogueNode):**
- ***NPCSpeech:*** String property representing the NPC's speech for the current node.
- ***PlayerAnswers:*** List of strings representing player response options.
- ***NextNodes:*** Dictionary linking player answers to subsequent dialogue nodes.
- ***SerializableNextNodes:*** List of SerializableKeyValuePair objects for XML serialization.

##### **Methods (DialogueNode):**
- ***PopulateNextNode():*** Populates the NextNodes dictionary from SerializableNextNodes.
- ***AddPlayerAnswer(string answer, DialogueNode? nextNode):*** Adds a player answer and its linked node.

### **SerializableKeyValuePair:** 
- Represents a serializable key-value pair (two keys in one).

##### **Properties (SerializableKeyValuePair):**
- ***Key:*** String property representing the key in the key-value pair.
- ***Value:*** DialogueNode property representing the value in the key-value pair.

### **Program**
- Contains the Main method as an entry point for the application.

##### **Methods (Program):**
- ***SaveDialogueToFile(string fileName, DialogueNode rootNode):*** Serializes a DialogueNode object to an XML file.
- ***LoadDialogueFromFile(string fileName):*** Deserializes a DialogueNode object from an XML file.
- ***ConductConversation(DialogueNode? currentNode):*** Conducts a conversation using the provided dialogue nodes.
 
## **Usage**
#### **1. Creating Dialogue Nodes:**
- Create instances of DialogueNode and set NPCSpeech to represent NPC dialogue.
- Use AddPlayerAnswer to add player responses and link them to subsequent nodes.

#### **2. Saving/Loading Dialogue:**
- Use SaveDialogueToFile to serialize a dialogue node to an XML file.
- Use LoadDialogueFromFile to deserialize a dialogue node from an XML file.

#### **3. Conducting Conversations:**
- Use ConductConversation to simulate a conversation.
- NPCs' speech and available player options are displayed in the console.
- Player inputs determine the flow of the conversation.

## **Unity Integration**
##### **To integrate this system into Unity:**
- Adapt the code to Unity's MonoBehaviour scripts.
- Use Unity UI elements (e.g., Text, Buttons) to display NPC speech and player responses.
- Replace console interactions with Unity UI events to capture player input.

## **Example Usage (Console)**
- The provided Main method demonstrates the usage by creating dialogue nodes, saving/loading to/from XML, and conducting a conversation via console input/output.

## **Note:**
- This code is a console application and requires modifications to integrate with Unity.
- Unity UI elements and event handling should replace console interactions for user input and display.