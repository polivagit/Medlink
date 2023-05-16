<?php

namespace App\Rules;

use Illuminate\Contracts\Validation\Rule;

class CustomRules implements Rule
{
    protected $min;
    protected $max;

    public function __construct($min, $max)
    {
        $this->min = $min;
        $this->max = $max;
    }

    public function passes($attribute, $value)
    {
        return $value >= $this->min && $value <= $this->max;
    }

    public function message()
    {
        return "The :attribute must be between {$this->min} and {$this->max}.";
    }
}

?>