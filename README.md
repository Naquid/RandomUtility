# RandomUtility
Utility for making random variables easier with distribution curves

--RandomFloat--
Lightweight class that can be used as a regular float. Value us cached after first use and will return 
same value until restart or GetNewRandom() is called.
Ex.
```ruby
    using RandomUtility;

    public RandomFloat randomSpeed;

    void Update()
    {
      transform.position += transform.forward * randomSpeed * Time.delta;
    }
```

--RandomFloat Range Attribute--
Can be applied to RandomFloat to give a nice slider with max/min values in the inspector.
```ruby
    [RandomFloatRange(0.0f, 5.0f)]
    using RandomUtility;

    public RandomFloat randomSpeed;
```
--RandomFloatDistribution--
Same as RandomFloat with support for controlling the random distribution using a few different methods.

  1. Normal
    Bell shaped distribution with control over mean and stdDev
  
    ![Alt Text](https://i.gyazo.com/e1d0d9e52be3fab6db0c7ef807072f91.gif)
    
  2. Slope
   Created a linear slope
  
   ![Alt Text](https://i.gyazo.com/e8e97adeaaf0a81a7b37cbb21fb6f95c.gif)
   
  3. Curve
    Using a custom probability curve
  
    ![Alt Text](https://i.gyazo.com/55e8fca888c55a20a0d289fc2e12b198.gif)
    
  4. Exp
     Exponential curve
      
     ![Alt Text](https://i.gyazo.com/d1e7f4faafd80f7d7990d28393104ae6.gif)
    
  5. List
    Use a list with predefined values and assign relative probabilities for them to get randomized
  
    ![Alt Text](https://i.gyazo.com/031de47da31c9cb00bc0fdafe4f3df92.gif)
