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

   ![Alt Text](https://github.com/Naquid/RandomUtility/tree/master/Readme/RandomFloatRange.png)
   
--RandomFloatDistribution--
Same as RandomFloat with support for controlling the random distribution using a few different methods.

  1. Normal
    Bell shaped distribution with control over mean and stdDev
  
   ![Alt Text](https://github.com/Naquid/RandomUtility/tree/master/Readme/normal.png)
    
  2. Slope
   Created a linear slope
  
   ![Alt Text](https://github.com/Naquid/RandomUtility/tree/master/Readme/slope.png)
   
  3. Curve
    Using a custom probability curve
  
   ![Alt Text](https://github.com/Naquid/RandomUtility/tree/master/Readme/Curve.png)
    
  4. Exp
     Exponential curve
      
   ![Alt Text](https://github.com/Naquid/RandomUtility/tree/master/Readme/Exp.png)
    
  5. List
    Use a list with predefined values and assign relative probabilities for them to get randomized
  
   ![Alt Text](https://github.com/Naquid/RandomUtility/tree/master/Readme/List.png)
