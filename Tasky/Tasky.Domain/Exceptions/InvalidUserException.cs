using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Domain.Exceptions;

public class InvalidUserException(string message) : DomainException(message);
