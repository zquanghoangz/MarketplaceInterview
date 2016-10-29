Reebonz Marketplace Interview Test
=========================

Part 1 - Coding
---------------
**Setup:**

Open Reebonz.Interview\Reebonz.Interview.sln

**Scenario:**

The existing Reebonz.Interview shopping cart (hit F5 in Visual Studio to see it) allows you to add and remove items. As you add each item you can set its Shipping method.
The available shipping options are stored in App_Data\Shipping.xml.

**Objectives:**

1. Add a new Shipping Option to the application code. The Shipping Option should behave the same as the PerRegion Option except that if there is at least one other item in the basket with the same Shipping Option and the same Supplier and Region, 50 pence should be deducted from the shipping. Keep in mind the ability to change the parameters.
2. Write a unit test for the new code.
3. Form some opinions about how the code has been put together for discussion. Note that we are not looking for any particular criticisms (there are no "trick" mistakes, though there may be some genuine ones!).
4. When done, get the code back to us.  Bonus points for a pull request, but if you're new to DVCS don't worry - just zip up the code and email it.

**Time allowed:**

There is no time limit, but hopefully it shouldn't take you longer than about an hour.

**Tips:**

1. App_Data\Shipping.xml is created with the CreateSampleData unit test. You should add to this to create your extended version of this file.
2. Don't forget to add any new Shipping types to the KnownTypes method on ShippingBase.
3. The Controller action used for the page is the Index action on the HomeController.
4. All prices are in UK £ Pound currency. (1 pound equals 100 pence)


Part 2 - Design
---------------
We'd like the Shipping App to use the Unit of Work pattern.  For the purposes of this assignment, the Unit of Work pattern means:

*The framework takes care of persisting all data at the end of a defined "unit of work".*

In concrete terms, as the Shipping App is a web app, a "unit of work" will be a web request.  This means that the application code should not be explicitly saving the basket after modification, but instead the framework should somehow know that it has changed and save it at the end of the web request.

Please produce a document explaining how you might do this, including any restrictions your solution has or considerations as the app continued to be developed.  Your document can include diagrams, pseudo-code or just consist of words.   

Note that the important thing is your ideas and thinking and how you get them across, not the presentation / formatting of the document, so please don't worry unduly about this.

Any questions about either part of this assignment, please don't hesitate to ask.  Good luck!

Ryan (Tong Quang Hoang)'s Note
---------------

**Add new Shipping Option**

The new Shipping Option is 'SameRegion' in SameRegionShipping class: the idea is update function GetAmount to find the first item matched condition, then if it's not the current line item, will reduce shipping rate. The amount to reduce is configurable in App_Data\Shipping.xml in ReduceRate tag
Unit test is appled and all case run successfully

**Some discussion**

SameRegionShipping class can inherit from PerRegionShipping. In reality, it depends on how many functions have the same logic code in both class. In this case, I choosed not to inherit because it's not many duplicate logic code and independence to maintain in the future.

For serilization, we can use JSON instead of XML for lightly better performance and readable.