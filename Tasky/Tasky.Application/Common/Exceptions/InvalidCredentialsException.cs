using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Exceptions;

public class InvalidCredentialsException() : Exception("Invalid login or password.");
