![image](https://github.com/user-attachments/assets/abd22b94-464d-49da-814b-cd3aa033d4e0)

So we start by creating our own user. We can choose between the 4 role types. ![image](https://github.com/user-attachments/assets/c906f0c4-fde4-408a-8a74-8ae5d6caf80a)

Both when registering and logging in we generate a bearer token which should be attached to authorize following requests. This token contains information about the users id and their role.

After we create our user we can inspect the token returned to us, it also helps to be attached when making requests ![image](https://github.com/user-attachments/assets/69f49cbb-7a39-4f22-ae50-b365d803c280)

So as a Writer and a Editor we are able to create articles. 

In this example we try to create a article as a subscriber (lowest tier user) and are permitted to do so.
![image](https://github.com/user-attachments/assets/85cc964b-a26f-4eab-b39a-429e6d2c20e7)

This is done by putting the authorization tag and listing only Editor and Writer as allowed.

![image](https://github.com/user-attachments/assets/f9d6e64b-b8f2-4c81-87a5-f39b3a83f48e)

As we can see that comments are not allowed to be posted without authorization

![image](https://github.com/user-attachments/assets/99676519-6273-4031-89f3-c6fa3f0c47a1)

As an authenticated subscriber we can delete our own comment

![image](https://github.com/user-attachments/assets/a81e3541-a151-48b7-ab69-f61601619372)

And with logical operations we ensure that if the id in our bearer token doesn't match the id of the creator of the comment we are accessing then we can't delete it, safe for an Editor.

![image](https://github.com/user-attachments/assets/9f56dd06-72fa-482c-bf6b-c70c8a4e45c0)










