Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports VazorTopShelf.Db.Models

Namespace Configurations
  Public Class ContactConfiguration
    Implements IEntityTypeConfiguration(Of Contact)

    Public Sub Configure(Builder As EntityTypeBuilder(Of Contact)) Implements IEntityTypeConfiguration(Of Contact).Configure
      Builder.Property(Function(Contact) Contact.FirstName).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(10)
      Builder.Property(Function(Contact) Contact.LastName).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(10)
      Builder.Property(Function(Contact) Contact.Email).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(50)
      Builder.Property(Function(Contact) Contact.PhoneArea).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(3)
      Builder.Property(Function(Contact) Contact.PhonePrefix).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(3)
      Builder.Property(Function(Contact) Contact.PhoneSuffix).IsRequired.HasDefaultValue(String.Empty).HasMaxLength(4)

      Builder.HasIndex(Function(Contact) New With {Contact.FirstName, Contact.LastName})
      Builder.HasIndex(Function(Contact) Contact.FirstName)
      Builder.HasIndex(Function(Contact) Contact.LastName)
    End Sub
  End Class
End Namespace
