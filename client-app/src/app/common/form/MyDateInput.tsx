import React from "react";
import { useField } from "formik";
import { Form, Label } from "semantic-ui-react";
import DatePicker, { ReactDatePickerProps } from "react-datepicker";

export default function MyDateInput(props: Partial<ReactDatePickerProps>) {
  //Partial make every single property optional
  const [field, meta, helpers] = useField(props.name!); //! use to no constrains rule for this particular property
  return (
    <Form.Field error={meta.touched && !!meta.error}>
      <DatePicker
        {...field}
        {...props}
        selected={(field.value && new Date(field.value)) || null}
        onChange={(value) => helpers.setValue(value)}
      />
      {meta.touched && meta.error ? (
        <Label basic color="red">
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
}
//Note
//!! in form.field error using to cast this into a boolean
